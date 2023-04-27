using accounting.Model;
using accounting.View;
using Npgsql;

namespace accounting.DataBase;

public class UserDataAccess : IDataAccess<User>
{
    private static readonly PgSqlConnection Conn = new();

    private readonly string _connectionString = Conn.ToString();

    private readonly IView<User> _userView = new UserView();

    public IEnumerable<User> GetAll()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);
        using var command = new NpgsqlCommand(
            "SELECT * FROM ad_user",
            connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
            yield return new User(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3)
            );
    }

    public IEnumerable<User> GetSomeLastRecords(int count)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);
        using var command = new NpgsqlCommand(
            "SELECT * FROM ad_user " +
            "(SELECT * FROM ad_user ORDER BY sid DESC LIMIT WHERE sid = @count)" +
            "AS T ORDER BY sid ASC",
            connection);
        command.Parameters.AddWithValue("@count", count);
        using var reader = command.ExecuteReader();
        while (reader.Read())
            yield return new User(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3)
            );
    }

    public User GetById(int sid)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);
        using var command = new NpgsqlCommand(
            "SELECT * FROM ad_user WHERE sid = @sid",
            connection);
        command.Parameters.AddWithValue("@sid", sid);
        using var reader = command.ExecuteReader();
        if (reader.Read())
             return new User(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3)
            );
        return null!;
    }

    public void Add(User entity)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command = new NpgsqlCommand(
            "INSERT INTO ad_user VALUES (" +
            "@sid," +
            "@display_name," +
            "@department," +
            "@sam_account_name)",
            connection);

        command.Parameters.AddWithValue("@sid", entity.Sid);
        command.Parameters.AddWithValue("@display_name", entity.DisplayName);
        command.Parameters.AddWithValue("@department", entity.Department);
        command.Parameters.AddWithValue("@sam_account_name", entity.SamAccountName);

        try
        {
            command.ExecuteNonQuery();
        }
        catch (PostgresException e)
        {
            _userView.ShowError("Error add user");
            _userView.ShowError(e.MessageText);
        }
    }

    public void Update(User entity)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command = new NpgsqlCommand(
            "UPDATE ad_user SET " +
            "display_name = @display_name," +
            "department = @department," +
            "sam_account_name = @sam_account_name" +
            "WHERE sid = @sid",
            connection);

        command.Parameters.AddWithValue("@sid", entity.Sid);
        command.Parameters.AddWithValue("@display_name", entity.DisplayName);
        command.Parameters.AddWithValue("@department", entity.Department);
        command.Parameters.AddWithValue("@sam_account_name", entity.SamAccountName);

        var rowsAffected = command.ExecuteNonQuery();
        if (rowsAffected == 0)
            _userView.ShowError("No user updated. User with given Sid does not exist in the database.");
    }

    public void Delete(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        ConnectionOpen(connection);

        using var command = new NpgsqlCommand(
            "DELETE FROM ad_user  WHERE sid = @sid",
            connection);

        command.Parameters.AddWithValue("@sid", id);

        var rowsAffected = command.ExecuteNonQuery();
        if (rowsAffected == 0)
            _userView.ShowError($"No user fount with sid = {id}.");
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