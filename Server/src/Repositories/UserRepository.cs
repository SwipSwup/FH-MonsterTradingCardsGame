using Shared.DTOs;
using System.Data;
using Npgsql;
using Server.DataBase;

namespace Server.Repositories;

public class UserRepository : IRepository<int, UserDto>
{
    public async Task<UserDto?> GetByIdAsync(int id)
    {
        const string query = $"SELECT user_id, username, password_hash, currency, mmr FROM users WHERE user_id = @UserId;";
        NpgsqlParameter[] parameters = new[] 
        {
            new NpgsqlParameter("@UserId", DbType.Int32) { Value = id }
        };

        DataTable result = await DataBaseManager.ExecuteQueryAsync(query, parameters);
        if (result.Rows.Count == 0)
            return null;

        DataRow row = result.Rows[0];
        return new UserDto
        {
            UserId = Convert.ToInt32(row["user_id"]),
            Username = row["username"].ToString()!,
            Password = row["password_hash"].ToString()!,
            Currency = (int)row["currency"],
            Mmr = (int)row["mmr"]
        };
    }

    public async Task<UserDto?> GetByUsernameAsync(string username)
    {
        const string query = $"SELECT user_id, username, password_hash, currency, mmr FROM users WHERE username = @Username;";
        NpgsqlParameter[] parameters = new[] 
        {
            new NpgsqlParameter("@Username", DbType.String) { Value = username }
        };

        DataTable result = await DataBaseManager.ExecuteQueryAsync(query, parameters);
        if (result.Rows.Count == 0)
            return null;

        DataRow row = result.Rows[0];
        return new UserDto
        {
            UserId = Convert.ToInt32(row["user_id"]),
            Username = row["username"].ToString()!,
            Password = row["password_hash"].ToString()!,
            Currency = (int)row["currency"],
            Mmr = (int)row["mmr"]
        };
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        const string query = $"SELECT user_id, username, password_hash, currency, mmr FROM users;";
        DataTable result = await DataBaseManager.ExecuteQueryAsync(query);

        return result.AsEnumerable().Select(row => new UserDto
        {
            UserId = Convert.ToInt32(row["user_id"]),
            Username = row["username"].ToString()!,
            Password = row["password_hash"].ToString()!,
            Currency = (int)row["currency"],
            Mmr = (int)row["mmr"]
        });
    }
    
    public async Task<IEnumerable<UserDto>> FindUsersInMmrRangeAsync(int userMmt, int range)
    {
        // Calculate the lower and upper bounds for the MMR range
        int lowerBound = userMmt - range;
        int upperBound = userMmt + range;

        // Define the SQL query to get players within the MMR range
        string query = @"
        SELECT user_id, password_hash, currency, username, mmr
        FROM users
        WHERE mmr BETWEEN @LowerBound AND @UpperBound
        AND user_id != @PlayerId"; // Exclude the current player from the results

        // Define the parameters for the query
        NpgsqlParameter[] parameters = new NpgsqlParameter[]
        {
            new("@LowerBound", NpgsqlTypes.NpgsqlDbType.Integer) { Value = lowerBound },
            new("@UpperBound", NpgsqlTypes.NpgsqlDbType.Integer) { Value = upperBound },
            new("@PlayerId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = userMmt } // Exclude the player from the search
        };

        // Execute the query and get the result
        DataTable result = await DataBaseManager.ExecuteQueryAsync(query, parameters);

        // Convert the DataTable result into a list of UserDto objects
        List<UserDto> playersInRange = new List<UserDto>();
        foreach (DataRow row in result.Rows)
        {
            playersInRange.Add(new UserDto
            {
                UserId = (int)row["user_id"],
                Password = row["password_hash"].ToString()!,
                Currency = (int)row["currency"],
                Username = row["username"].ToString(),
                Mmr = (int)row["mmr"]
            });
        }

        return playersInRange;
    }


    public async Task<UserDto> AddAsync(UserDto entity)
    {
        const string query = $"INSERT INTO users (username, password_hash, currency, mmr) VALUES (@Username, @PasswordHash, @Currency, @Mmr) RETURNING user_id;";
        NpgsqlParameter[] parameters = new[] 
        {
            new NpgsqlParameter("@Username", DbType.String) { Value = entity.Username },
            new NpgsqlParameter("@PasswordHash", DbType.String) { Value = entity.Password },
            new NpgsqlParameter("@Currency", DbType.Int32) { Value = 200 },
            new NpgsqlParameter("@Mmr", DbType.Int32) { Value = 1200 }
        };

        object? userId = await DataBaseManager.ExecuteScalarAsync(query, parameters);
        if (userId == null)
            throw new Exception("Failed to insert user into the database.");

        entity.UserId = Convert.ToInt32(userId);
        return entity;
    }

    public async Task UpdateAsync(UserDto entity)
    {
        const string query = $"UPDATE users SET username = @Username, password_hash = @PasswordHash, currency = @Currency, mmr = @Mmr WHERE user_id = @UserId;";
        NpgsqlParameter[] parameters =
        [
            new("@Username", DbType.String) { Value = entity.Username },
            new("@PasswordHash", DbType.String) { Value = entity.Password },
            new("@Currency", DbType.Int32) { Value = entity.Currency },
            new("@Mmr", DbType.Int32) { Value = entity.Mmr },
            new("@UserId", DbType.Int32) { Value = entity.UserId }
        ];

        int affectedRows = await DataBaseManager.ExecuteNonQueryAsync(query, parameters);
        if (affectedRows == 0)
            throw new KeyNotFoundException($"User with ID {entity.UserId} not found.");
    }

    public async Task DeleteAsync(int id)
    {
        const string query = $"DELETE FROM users WHERE user_id = @UserId;";
        NpgsqlParameter[] parameters = new[] 
        {
            new NpgsqlParameter("@UserId", DbType.Int32) { Value = id }
        };

        int affectedRows = await DataBaseManager.ExecuteNonQueryAsync(query, parameters);
        if (affectedRows == 0)
            throw new KeyNotFoundException($"User with ID {id} not found.");
    }
}
