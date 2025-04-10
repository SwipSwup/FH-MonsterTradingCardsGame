using System.Data;
using Npgsql;

namespace Server.DataBase;

public static class DataBaseManager
{
    //private const string ConnectionString =
    //    "Host=localhost;Username=postgres;Password=admin;Database=postgres;Pooling=true;MinPoolSize=10;MaxPoolSize=100;";

    private static readonly string ConnectionString =
        Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
        ?? "Host=localhost;Username=postgres;Password=admin;Database=postgres;";
    
    private static NpgsqlCommand CreateCommands(string query, NpgsqlConnection connection,
        NpgsqlParameter[]? parameters)
    {
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.CommandTimeout = 15;

        if (parameters != null)
        { 
            command.Parameters.AddRange(parameters);
        }

        return command;
    }

    public static async Task<DataTable> ExecutePaginatedQueryAsync(string query,
        int size, int page, params NpgsqlParameter[]? parameters)
    {
        query += " LIMIT @PageSize OFFSET @Offset;";
    
        NpgsqlParameter[] paginationParameters =
        {
            new("@PageSize", NpgsqlTypes.NpgsqlDbType.Integer) { Value = size },
            new("@Offset", NpgsqlTypes.NpgsqlDbType.Integer) { Value = (page - 1) * size }
        };

        return await ExecuteQueryAsync(
            query,
            parameters?.Concat(paginationParameters).ToArray() ?? paginationParameters
        );
    }

    public static async Task<DataTable> ExecuteQueryAsync(string query, params NpgsqlParameter[]? parameters)
    {
        await using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();

        await using NpgsqlCommand command = CreateCommands(query, connection, parameters);
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();  

        DataTable dataTable = new DataTable();
        dataTable.Load(reader);  
        return dataTable;
    }

    // Execute a non-query (e.g., INSERT, UPDATE, DELETE)
    public static async Task<int> ExecuteNonQueryAsync(string query, params NpgsqlParameter[]? parameters)
    {
        await using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();
        
        await using NpgsqlCommand command = CreateCommands(query, connection, parameters);

        return await command.ExecuteNonQueryAsync();
    }

    // Execute a scalar query (e.g., return a single value)
    public static async Task<object?> ExecuteScalarAsync(string query, params NpgsqlParameter[]? parameters)
    {
        await using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();
        
        await using NpgsqlCommand command = CreateCommands(query, connection, parameters);
        /*if (parameters != null)
        {
            command.Parameters.AddRange(parameters);
        }*/

        return await command.ExecuteScalarAsync();
    }
}