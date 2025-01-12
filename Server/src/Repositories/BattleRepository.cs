using Shared.DTOs;
using System.Data;
using Npgsql;
using Server.DataBase;

namespace Server.Repositories
{
    public class BattleRepository : IRepository<int, BattleDto>
    {
        public async Task<BattleDto?> GetByIdAsync(int id)
        {
            const string query = "SELECT battle_id, player_1_id, player_2_id, winner_id FROM battles WHERE battle_id = @BattleId;";
            NpgsqlParameter[] parameters = new[] 
            {
                new NpgsqlParameter("@BattleId", DbType.Int32) { Value = id }
            };

            DataTable result = await DataBaseManager.ExecuteQueryAsync(query, parameters);
            if (result.Rows.Count == 0)
                return null;

            DataRow row = result.Rows[0];
            return new BattleDto
            {
                BattleId = Convert.ToInt32(row["battle_id"]),
                Player1Id = Convert.ToInt32(row["player_1_id"]),
                Player2Id = Convert.ToInt32(row["player_2_id"]),
                WinnerId = row.IsNull("winner_id") ? null : (int?)Convert.ToInt32(row["winner_id"]),
            };
        }

        public async Task<IEnumerable<BattleDto>> GetAllAsync()
        {
            const string query = "SELECT battle_id, player_1_id, player_2_id, winner_id FROM battles;";
            DataTable result = await DataBaseManager.ExecuteQueryAsync(query);

            return result.AsEnumerable().Select(row => new BattleDto
            {
                BattleId = Convert.ToInt32(row["battle_id"]),
                Player1Id = Convert.ToInt32(row["player_1_id"]),
                Player2Id = Convert.ToInt32(row["player_2_id"]),
                WinnerId = row.IsNull("winner_id") ? null : (int?)Convert.ToInt32(row["winner_id"]),
            });
        }
        
        public async Task LogBattleRoundAsync(BattleRoundDto battleRoundDto)
        {
            string query = @"
        INSERT INTO battle_rounds (battle_id, round_number, card_1_id, card_2_id, winner_card_id, result)
        VALUES (@BattleId, @RoundNumber, @Card1Id, @Card2Id, @WinnerCardId, @Result)";
    
            var parameters = new[]
            {
                new NpgsqlParameter("@BattleId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = battleRoundDto.BattleId },
                new NpgsqlParameter("@RoundNumber", NpgsqlTypes.NpgsqlDbType.Integer) { Value = battleRoundDto.RoundNumber },
                new NpgsqlParameter("@Card1Id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = battleRoundDto.Card1Id },
                new NpgsqlParameter("@Card2Id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = battleRoundDto.Card2Id },
                new NpgsqlParameter("@WinnerCardId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = battleRoundDto.WinnerCardId },
                new NpgsqlParameter("@Result", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = battleRoundDto.Result }
            };

            await DataBaseManager.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<BattleDto> AddAsync(BattleDto entity)
        {
            const string query = @"
                INSERT INTO battles (player_1_id, player_2_id, winner_id) 
                VALUES (@Player1Id, @Player2Id, @WinnerId) 
                RETURNING battle_id;";
            
            NpgsqlParameter[] parameters = new[] 
            {
                new NpgsqlParameter("@Player1Id", DbType.Int32) { Value = entity.Player1Id },
                new NpgsqlParameter("@Player2Id", DbType.Int32) { Value = entity.Player2Id },
                new NpgsqlParameter("@WinnerId", DbType.Int32) { Value = entity.WinnerId.HasValue ? (object)entity.WinnerId.Value : DBNull.Value },
            };

            object? battleId = await DataBaseManager.ExecuteScalarAsync(query, parameters);
            if (battleId == null)
                throw new Exception("Failed to insert battle into the database.");

            entity.BattleId = Convert.ToInt32(battleId);
            return entity;
        }

        public async Task UpdateAsync(BattleDto entity)
        {
            const string query = @"
                UPDATE battles
                SET player_1_id = @Player1Id, player_2_id = @Player2Id, winner_id = @WinnerId
                WHERE battle_id = @BattleId;";

            NpgsqlParameter[] parameters = new[] 
            {
                new NpgsqlParameter("@Player1Id", DbType.Int32) { Value = entity.Player1Id },
                new NpgsqlParameter("@Player2Id", DbType.Int32) { Value = entity.Player2Id },
                new NpgsqlParameter("@WinnerId", DbType.Int32) { Value = entity.WinnerId.HasValue ? (object)entity.WinnerId.Value : DBNull.Value },
                new NpgsqlParameter("@BattleId", DbType.Int32) { Value = entity.BattleId }
            };

            int affectedRows = await DataBaseManager.ExecuteNonQueryAsync(query, parameters);
            if (affectedRows == 0)
                throw new KeyNotFoundException($"Battle with ID {entity.BattleId} not found.");
        }

        public async Task DeleteAsync(int id)
        {
            const string query = "DELETE FROM battles WHERE battle_id = @BattleId;";
            NpgsqlParameter[] parameters = new[] 
            {
                new NpgsqlParameter("@BattleId", DbType.Int32) { Value = id }
            };

            int affectedRows = await DataBaseManager.ExecuteNonQueryAsync(query, parameters);
            if (affectedRows == 0)
                throw new KeyNotFoundException($"Battle with ID {id} not found.");
        }
    }
}
