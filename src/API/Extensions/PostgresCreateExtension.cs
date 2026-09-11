using Npgsql;

namespace API.Extensions
{
    public static class PostgresCreateExtension
    {    
        public static async Task EnsureDatabaseExistsAsync(string connectionString)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString);

            var databaseName = builder.Database;

            builder.Database = "postgres";

            await using var connection = new NpgsqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            var existsCommand = new NpgsqlCommand(
                "SELECT 1 FROM pg_database WHERE datname = @dbname", connection);

            existsCommand.Parameters.AddWithValue("dbname", databaseName
                ?? throw new Exception("databaseName is null"));

            var exists = await existsCommand.ExecuteScalarAsync();

            if (exists is null)
            {
                var createCommand = new NpgsqlCommand(
                    $"CREATE DATABASE \"{databaseName}\"", connection);

            await createCommand.ExecuteNonQueryAsync();
            }
        }
    }
}