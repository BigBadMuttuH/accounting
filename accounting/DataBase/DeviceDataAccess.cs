using accounting.Model;
using Npgsql;

namespace accounting.DataBase;

public class DeviceDataAccess : IDataAccess<Device>
{
    public static PgSQLConnectionString ConnString = new()
    {
        Host = "localhost",
        Port = "5432",
        Database = "acl",
        User = "postgres",
        Password = "postgres"
    };

    private readonly string _connectionString = ConnString.ToString();

    public void Add(Device entity)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command =
            new NpgsqlCommand(
                "INSERT INTO device (id, model, vid, pid, serial_number, inventory_number) VALUES (@id, @model, @vid, @pid, @serial_number, @inventory_number)",
                connection);
        command.Parameters.AddWithValue("@id", entity.Id);
        command.Parameters.AddWithValue("@model", entity.Model);
        command.Parameters.AddWithValue("@vid", entity.Vid);
        command.Parameters.AddWithValue("@pid", entity.Pid);
        command.Parameters.AddWithValue("@serial_number", entity.SerialNumber);
        command.Parameters.AddWithValue("@inventory_number", entity.InventoryNumber);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (PostgresException e)
        {
            if (e.Code == "23505" && e.ConstraintName == "device_serial_number_key")
            {
                Console.WriteLine(
                    "\nУстройство с таким серийным номером уже существует в базе данных." +
                    "\nПожалуйста, повторите попытку с другим серийным номером.");
            }
            else
            {
                throw new Exception("Ошибка при выполнении запроса.", e);
            }
        }
        finally
        {
            Console.ReadKey();
        }
    }

    public void Delete(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);
        using var command = new NpgsqlCommand("DELETE FROM device WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);

        var rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected == 0) throw new Exception($"No device found with id {id}");
    }

    public IEnumerable<Device> GetAll()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command = new NpgsqlCommand("SELECT * FROM device ORDER BY id", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
            yield return new Device(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5)
            );
    }

    public IEnumerable<Device> GetSomeLastRecords(int count)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command =
            new NpgsqlCommand("SELECT * FROM (SELECT * FROM device ORDER BY id DESC LIMIT @count) AS T ORDER BY id ASC",
                connection);
        command.Parameters.AddWithValue("@count", count);
        using var reader = command.ExecuteReader();
        while (reader.Read())
            yield return new Device(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5)
            );
    }


    public Device GetById(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);
        using var command = new NpgsqlCommand("SELECT * FROM device WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        using var reader = command.ExecuteReader();
        if (reader.Read())
            return new Device(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5)
            );
        return null!;
    }

    public void Update(Device entity)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);
        using var command =
            new NpgsqlCommand(
                "UPDATE device SET model = @model, vid = @vid, pid = @pid, serial_number = @serial_number, inventory_number = @inventory_number WHERE id = @id",
                connection);
        command.Parameters.AddWithValue("@model", entity.Model);
        command.Parameters.AddWithValue("@vid", entity.Vid);
        command.Parameters.AddWithValue("@pid", entity.Pid);
        command.Parameters.AddWithValue("@serial_number", entity.SerialNumber);
        command.Parameters.AddWithValue("@inventory_number", entity.InventoryNumber);
        command.Parameters.AddWithValue("@id", entity.Id);

        var rowsAffected = command.ExecuteNonQuery();
        if (rowsAffected == 0)
            throw new Exception("No rows updated. Device with given Id does not exist in the database.");
    }

    /// <summary>
    ///     Trying open connection to DataBase
    /// </summary>
    /// <param name="connection"></param>
    /// <exception cref="Exception"></exception>
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