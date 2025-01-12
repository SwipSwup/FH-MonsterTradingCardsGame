using System.Data;
using Npgsql;
using Server.DataBase;
using Shared.Card;
using Shared.DTOs;

namespace Server.Repositories
{
    public class CardRepository : IRepository<int, CardDto>
    {
        public async Task<CardDto?> GetByIdAsync(int id)
        {
            string query = "SELECT card_id, card_name, species_id, damage, rarity_id, element_id, card_type_id FROM cards WHERE card_id = @Id";
            NpgsqlParameter[] parameters = [new("@Id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = id }];

            DataTable result = await DataBaseManager.ExecuteQueryAsync(query, parameters);

            if (result.Rows.Count == 0)
                return null;

            DataRow row = result.Rows[0];
            return new CardDto
            {
                CardId = (int)row["card_id"],
                CardName = row["card_name"].ToString(),
                Species = (Species)row["species_id"],
                Damage = (int)row["damage"],
                Rarity = (Rarity)row["rarity_id"],
                Element = (Element)row["element_id"],
                CardType = (CardType)row["card_type_id"]
            };
        }

        public async Task<IEnumerable<CardDto>> GetAllAsync()
        {
            string query = "SELECT card_id, card_name, species_id, damage, rarity_id, element_id, card_type_id FROM cards";
            DataTable result = await DataBaseManager.ExecuteQueryAsync(query);

            List<CardDto> cards = new List<CardDto>();
            foreach (DataRow row in result.Rows)
            {
                cards.Add(new CardDto
                {
                    CardId = (int)row["card_id"],
                    CardName = row["card_name"].ToString(),
                    Species = (Species)row["species_id"],
                    Damage = (float)row["damage"],
                    Rarity = (Rarity)row["rarity_id"],
                    Element = (Element)row["element_id"],
                    CardType = (CardType)row["card_type_id"]
                });
            }

            return cards;
        }

        public async Task<CardDto> AddAsync(CardDto entity)
        {
            string query = @"
                INSERT INTO cards (card_name, species_id, damage, rarity_id, element_id, card_type_id)
                VALUES (@CardName, @SpeciesId, @Damage, @RarityId, @ElementId, @CardTypeId) RETURNING card_id";
            
            NpgsqlParameter[] parameters =
            [
                new("@CardName", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = entity.CardName },
                new("@SpeciesId", entity.Species == null ? DBNull.Value : (int)entity.Species),
                new("@Damage", NpgsqlTypes.NpgsqlDbType.Real) { Value = entity.Damage },
                new("@RarityId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = (int)entity.Rarity },
                new("@ElementId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = (int)entity.Element },
                new("@CardTypeId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = (int)entity.CardType }
            ];

            object? result = await DataBaseManager.ExecuteScalarAsync(query, parameters);
            
            if (result != null && int.TryParse(result.ToString(), out int cardId))
            {
                entity.CardId = cardId;
            }
            
            return entity;
        }

        public async Task UpdateAsync(CardDto entity)
        {
            string query = @"
                UPDATE cards
                SET card_name = @CardName, species_id = @SpeciesId, damage = @Damage, 
                    rarity_id = @RarityId, element_id = @ElementId, card_type_id = @CardTypeId
                WHERE card_id = @CardId";
            
            NpgsqlParameter[] parameters =
            [
                new("@CardId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = entity.CardId },
                new("@CardName", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = entity.CardName },
                new("@SpeciesId", entity.Species == null ? DBNull.Value : (int)entity.Species),
                new("@Damage", NpgsqlTypes.NpgsqlDbType.Real) { Value = entity.Damage },
                new("@RarityId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = (int)entity.Rarity },
                new("@ElementId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = (int)entity.Element },
                new("@CardTypeId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = (int)entity.CardType }
            ];

            await DataBaseManager.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM cards WHERE card_id = @CardId";
            NpgsqlParameter[] parameters = new NpgsqlParameter[] { new("@CardId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = id } };

            await DataBaseManager.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<int?> GetCardIdIfExistsAsync(string cardName, float damage, int? speciesId, int rarityId, int elementId, int cardTypeId)
        {
            string query = @"
                SELECT card_id
                FROM cards
                WHERE card_name = @CardName AND species_id = @SpeciesId AND rarity_id = @RarityId 
                AND element_id = @ElementId AND card_type_id = @CardTypeId AND damage = @Damage";
            
            NpgsqlParameter[] parameters =
            [
                new("@CardName", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = cardName },
                new("@SpeciesId", speciesId == null ? DBNull.Value : speciesId),
                new("@RarityId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = rarityId },
                new("@Damage", NpgsqlTypes.NpgsqlDbType.Real) { Value = damage },
                new("@ElementId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = elementId },
                new("@CardTypeId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = cardTypeId }
            ];

            object? result = await DataBaseManager.ExecuteScalarAsync(query, parameters);
            return result != null ? (int?)result : null;
        }
    }
}
