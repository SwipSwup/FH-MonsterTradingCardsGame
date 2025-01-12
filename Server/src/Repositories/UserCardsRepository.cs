using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Shared.DTOs;
using Server.DataBase;
using Npgsql;
using Shared.Card;

namespace Server.Repositories
{
    public class UserCardsRepository : IRepository<int, UserCardsDto>
    {
        public async Task<UserCardsDto?> GetByIdAsync(int id)
        {
            string query = @"
                SELECT uc.user_id, uc.card_id, uc.is_in_deck, uc.is_in_trade
                FROM user_cards uc
                WHERE uc.user_id = @UserId AND uc.card_id = @CardId;";

            NpgsqlParameter[] parameters =
            {
                new("@UserId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = id }
            };

            DataTable result = await DataBaseManager.ExecuteQueryAsync(query, parameters);

            if (result.Rows.Count == 0)
                return null;

            DataRow row = result.Rows[0];

            return new UserCardsDto
            {
                UserId = (int)row["user_id"],
                CardId = (int)row["card_id"],
                IsInDeck = (bool)row["is_in_deck"],
                IsInTrade = (bool)row["is_in_trade"]
            };
        }


        public async Task<UserCardsDto?> GetByIdAsync(int userId, int cardId)
        {
            string query = @"
                SELECT uc.user_id, uc.card_id, uc.is_in_deck, uc.is_in_trade
                FROM user_cards uc
                WHERE uc.user_id = @UserId AND uc.card_id = @CardId;";

            NpgsqlParameter[] parameters =
            {
                new("@UserId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = userId },
                new("@CardId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = cardId }
            };

            DataTable result = await DataBaseManager.ExecuteQueryAsync(query, parameters);

            if (result.Rows.Count == 0)
                return null;

            DataRow row = result.Rows[0];

            return new UserCardsDto
            {
                UserId = (int)row["user_id"],
                CardId = (int)row["card_id"],
                IsInDeck = (bool)row["is_in_deck"],
                IsInTrade = (bool)row["is_in_trade"]
            };
        }

        public async Task<IEnumerable<UserCardsDto>> GetAllByUserIdAsync(int userId)
        {
            string query = @"
                SELECT uc.user_id, uc.card_id, uc.is_in_deck, uc.is_in_trade
                FROM user_cards uc WHERE uc.user_id = @UserId;";


            NpgsqlParameter[] parameters =
            {
                new("@UserId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = userId },
            };

            DataTable result = await DataBaseManager.ExecuteQueryAsync(query, parameters);

            List<UserCardsDto> userCards = new();

            foreach (DataRow row in result.Rows)
            {
                userCards.Add(new UserCardsDto
                {
                    UserId = (int)row["user_id"],
                    CardId = (int)row["card_id"],
                    IsInDeck = (bool)row["is_in_deck"],
                    IsInTrade = (bool)row["is_in_trade"]
                });
            }

            return userCards;
        }

        public async Task<IEnumerable<CardDto>> GetCardsInDeckAsync(int userId)
        {
            string query = @"
                SELECT c.card_id, c.card_name, c.species_id, c.damage, c.rarity_id, c.element_id, c.card_type_id
                FROM user_cards uc
                JOIN cards c ON uc.card_id = c.card_id
                WHERE uc.user_id = @UserId AND is_in_deck = true;";

            NpgsqlParameter[] parameters = new NpgsqlParameter[]
            {
                new("@UserId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = userId }
            };

            DataTable result = await DataBaseManager.ExecuteQueryAsync(query, parameters);

            List<CardDto> cards = new List<CardDto>();

            foreach (DataRow row in result.Rows)
            {
                cards.Add(new CardDto
                {
                    CardId = (int)row["card_id"],
                    CardName = row["card_name"].ToString(),
                    Species = row["species_id"] != DBNull.Value ? (Species?)(int)row["species_id"] : null,
                    Damage = row["damage"] != DBNull.Value ? Convert.ToSingle(row["damage"]) : 0f, 
                    Rarity = (Rarity)row["rarity_id"],
                    Element = (Element)row["element_id"],
                    CardType = (CardType)row["card_type_id"]
                });
            }

            return cards;
        }

        public async Task<IEnumerable<UserCardsDto>> GetAllAsync()
        {
            string query = @"
                SELECT uc.user_id, uc.card_id, uc.is_in_deck, uc.is_in_trade
                FROM user_cards uc;";

            DataTable result = await DataBaseManager.ExecuteQueryAsync(query);

            List<UserCardsDto> userCards = new();

            foreach (DataRow row in result.Rows)
            {
                userCards.Add(new UserCardsDto
                {
                    UserId = (int)row["user_id"],
                    CardId = (int)row["card_id"],
                    IsInDeck = (bool)row["is_in_deck"],
                    IsInTrade = (bool)row["is_in_trade"]
                });
            }

            return userCards;
        }


        public async Task<UserCardsDto> AddAsync(UserCardsDto entity)
        {
            string query = @"
                INSERT INTO user_cards (user_id, card_id, is_in_deck, is_in_trade)
                VALUES (@UserId, @CardId, @IsInDeck, @IsInTrade);";

            NpgsqlParameter[] parameters =
            {
                new("@UserId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = entity.UserId },
                new("@CardId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = entity.CardId },
                new("@IsInDeck", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = entity.IsInDeck },
                new("@IsInTrade", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = entity.IsInTrade }
            };

            await DataBaseManager.ExecuteNonQueryAsync(query, parameters);

            return entity;
        }

        public async Task UpdateAsync(UserCardsDto entity)
        {
            string query = @"
                UPDATE user_cards
                SET is_in_deck = @IsInDeck, is_in_trade = @IsInTrade
                WHERE user_id = @UserId AND card_id = @CardId;";

            NpgsqlParameter[] parameters =
            {
                new("@UserId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = entity.UserId },
                new("@CardId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = entity.CardId },
                new("@IsInDeck", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = entity.IsInDeck },
                new("@IsInTrade", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = entity.IsInTrade }
            };

            await DataBaseManager.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task DeleteAsync(int id)
        {
            string query = @"
                DELETE FROM user_cards
                WHERE user_id = @UserId AND card_id = @CardId;";

            NpgsqlParameter[] parameters =
            {
                new("@UserId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = id }
            };

            await DataBaseManager.ExecuteNonQueryAsync(query, parameters);
        }
    }
}