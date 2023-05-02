using accounting.Model;
using accounting.View;
using Npgsql;

namespace accounting.DataBase;

public class AccountingDataAccess : IDataAccess<Accounting>
{
    private static readonly PgSqlConnection Conn = new();

    private readonly IView<Accounting> _accountingView = new AccountingView();

    private readonly string _connectionString = Conn.ToString();

    public IEnumerable<Accounting> GetAll()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand(
            @"SELECT * FROM accounting_view", connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var accountingId = reader.GetInt32(0);
            var userId = reader.GetInt32(1);
            var deviceId = reader.GetInt32(2);
            var connectionPermissionId = reader.GetInt32(3);
            var disconnectionPermissionId = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4);
            var connectionPermissionDate = reader.GetDateTime(5);
            var displayName = reader.GetString(6);
            var department = reader.GetString(7);
            var inventoryNumber = reader.GetString(8);
            var serialNumber = reader.GetString(9);
            var connectionPermissionNumber = reader.GetString(10);
            var connectionPermissionUrl = reader.GetString(11);
            var disconnectionPermissionNumber = reader.IsDBNull(12) ? "" : reader.GetString(12);
            var disconnectionPermissionUrl = reader.IsDBNull(13) ? "" : reader.GetString(13);

            var user = new User(
                userId,
                displayName,
                department,
                "");
            var device = new Device(
                deviceId,
                "",
                "",
                "",
                inventoryNumber,
                serialNumber);
            var connectionPermission = new ConnectionPermission(
                connectionPermissionId,
                connectionPermissionNumber,
                connectionPermissionDate,
                "",
                connectionPermissionUrl);
            var disconnectionPermission = disconnectionPermissionId.HasValue
                ? new ConnectionPermission(
                    disconnectionPermissionId.Value,
                    disconnectionPermissionNumber,
                    null,
                    "",
                    disconnectionPermissionUrl)
                : new ConnectionPermission();

            yield return new Accounting(accountingId, user, device, connectionPermission, disconnectionPermission);
        }
    }

    public IEnumerable<Accounting> GetSomeLastRecords(int i)
    {
        throw new NotImplementedException();
    }

    public Accounting GetById(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand(
            @"SELECT * FROM accounting_view 
                    WHERE a.id = @id", connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (!reader.Read()) throw new ArgumentException($"Accounting record with id={id} not found");

        var accountingId = reader.GetInt32(0);
        var userId = reader.GetInt32(1);
        var deviceId = reader.GetInt32(2);
        var connectionPermissionId = reader.GetInt32(3);
        var disconnectionPermissionId = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4);
        var connectionPermissionDate = reader.GetDateTime(5);
        var displayName = reader.GetString(6);
        var department = reader.GetString(7);
        var inventoryNumber = reader.GetString(8);
        var serialNumber = reader.GetString(9);
        var connectionPermissionNumber = reader.GetString(10);
        var connectionPermissionUrl = reader.GetString(11);
        var disconnectionPermissionNumber = reader.IsDBNull(12) ? null : reader.GetString(12);
        var disconnectionPermissionUrl = reader.GetString(13);

        var user = new User(
            userId,
            displayName,
            department,
            "");
        var device = new Device(
            deviceId,
            "",
            "",
            "",
            serialNumber,
            inventoryNumber);
        var connectionPermission = new ConnectionPermission(
            connectionPermissionId,
            connectionPermissionNumber,
            connectionPermissionDate,
            "",
            connectionPermissionUrl);
        var disconnectionPermission = disconnectionPermissionId.HasValue
            ? new ConnectionPermission(
                disconnectionPermissionId.Value,
                disconnectionPermissionNumber,
                null,
                "",
                disconnectionPermissionUrl)
            : null;

        return new Accounting(accountingId, user, device, connectionPermission, disconnectionPermission);
    }

    public void Add(Accounting entity)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command = new NpgsqlCommand(
            "INSERT INTO accounting VALUES (" +
            "@id, @user_id, @device_id, @connection_permission_id, @disconnection_permission_id)", connection);
        command.Parameters.AddWithValue("@id", entity.Id);
        command.Parameters.AddWithValue("@user_id", entity.User.Sid);
        command.Parameters.AddWithValue("@device_id", entity.Device.Id);
        command.Parameters.AddWithValue("@connection_permission_id", entity.ConnectionPermission.Id);
        command.Parameters.AddWithValue("@disconnection_permission_id", entity.DisconnectionPermission.Id);

        try
        {
            command.ExecuteNonQuery();
        }
        catch (PostgresException e)
        {
            _accountingView.ShowError("Error add accounting.");
            _accountingView.ShowMessage(e.MessageText);
        }
    }

    public void Update(Accounting entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
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