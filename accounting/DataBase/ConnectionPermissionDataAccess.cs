using accounting.Model;
using accounting.View;
using Npgsql;

namespace accounting.DataBase;

public class ConnectionPermissionDataAccess : IDataAccess<ConnectionPermission>
{
    private static readonly PgSqlConnection ConnString = new();

    private readonly IView<ConnectionPermission> _connectionPermissionView = new ConnectionPermissionView();

    private readonly string _connectionString = ConnString.ToString();

    public IEnumerable<ConnectionPermission> GetAll()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command = new NpgsqlCommand("SELECT * FROM connection_permission ORDER BY id", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
            yield return new ConnectionPermission(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetDateTime(2),
                reader.GetString(3),
                reader.GetString(4)
            );
    }

    public IEnumerable<ConnectionPermission> GetSomeLastRecords(int count)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command =
            new NpgsqlCommand(
                @"SELECT * FROM
                            (SELECT * FROM connection_permission ORDER BY id DESC LIMIT @count) AS T
                        ORDER BY id ASC",
                connection);
        command.Parameters.AddWithValue("@count", count);
        using var reader = command.ExecuteReader();
        while (reader.Read())
            yield return new ConnectionPermission(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetDateTime(2),
                reader.GetString(3),
                reader.GetString(4)
            );
    }

    public ConnectionPermission GetById(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command = new NpgsqlCommand(@"SELECT * FROM connection_permission
                                                      WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        using var reader = command.ExecuteReader();
        if (reader.Read())
            return new ConnectionPermission(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetDateTime(2),
                reader.GetString(3),
                reader.GetString(4)
            );
        return null!;
    }

    public void Add(ConnectionPermission entity)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command =
            new NpgsqlCommand(
                @"INSERT INTO connection_permission VALUES (
                            @id, @permissionNumber, @permissionDate, @registrationNumber, @url
                        )",
                connection);
        command.Parameters.AddWithValue("@id", entity.Id);
        command.Parameters.AddWithValue("@permissionNumber", entity.PermissionNumber);
        command.Parameters.AddWithValue("@permissionDate", entity.PermissionDate);
        command.Parameters.AddWithValue("@registrationNumber", entity.RegistrationNumber);
        command.Parameters.AddWithValue("@url", entity.Url);

        try
        {
            command.ExecuteNonQuery();
        }
        catch (PostgresException e)
        {
            _connectionPermissionView.ShowError("Error execute query");
            _connectionPermissionView.ShowError(e.MessageText);
        }
    }

    public void Update(ConnectionPermission entity)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command =
            new NpgsqlCommand(
                @"UPDATE connection_permission SET 
                permission_number = @permissionNumber,
                permission_date = @permissionDate,
                registration_number = @registrationNumber,
                url = @url 
                WHERE id = @id",
                connection);

        command.Parameters.AddWithValue("@permissionNumber", entity.PermissionNumber);
        command.Parameters.AddWithValue("@permissionDate", entity.PermissionDate);
        command.Parameters.AddWithValue("@registrationNumber", entity.RegistrationNumber);
        command.Parameters.AddWithValue("@url", entity.Url);
        command.Parameters.AddWithValue("@id", entity.Id);

        var rowsAffected = command.ExecuteNonQuery();
        if (rowsAffected == 0)
            _connectionPermissionView.ShowError($"No rows updated. \n" +
                                                $"Connection permission with given Id{entity.Id} does not exist in the database.");
    }

    public void Delete(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);
        using var command = new NpgsqlCommand(@"DELETE FROM connection_permission 
                                                      WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);

        var rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected == 0)
            _connectionPermissionView.ShowError($"No connection permission found with id {id}");
    }

    private static void ConnectionOpen(NpgsqlConnection connection)
    {
        try
        {
            connection.Open();
        }
        catch (Exception e)
        {
            throw new Exception("Fail to open database connection.", e);
        }
    }
}