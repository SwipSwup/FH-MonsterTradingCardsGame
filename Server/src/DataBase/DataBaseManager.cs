using Npgsql;

namespace Server.DataBase;

public static class DataBaseManager
{
    private static string _connectionString = "Host=localhost;Username=postgres;Password=admin;Database=mtcg";
    
    public static async Task<List<Dictionary<string, object>>> ExecuteSelectAsync(string query, Dictionary<string, object> parameters = null)
    {
        var result = new List<Dictionary<string, object>>();

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        }
                        result.Add(row);
                    }
                }
            }
        }

        return result;
    }

    // Execute an INSERT/UPDATE/DELETE query
    public static async Task<int> ExecuteNonQueryAsync(string query, Dictionary<string, object> parameters = null)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                return await command.ExecuteNonQueryAsync();
            }
        }
    }

    // Execute a scalar query (e.g., COUNT, MAX, etc.)
    public static async Task<object> ExecuteScalarAsync(string query, Dictionary<string, object> parameters = null)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                return await command.ExecuteScalarAsync();
            }
        }
    }
}