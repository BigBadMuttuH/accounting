using accounting.Model;

namespace accounting.DataBase;

public class ConnectionPermissionDataAccess : IDataAccess<ConnectionPermission>
{
    private static readonly PgSQLConnection ConnString = new();

    private readonly string _connectionString = ConnString.ToString();

    public IEnumerable<ConnectionPermission> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ConnectionPermission> GetSomeLastRecords(int i)
    {
        throw new NotImplementedException();
    }

    public ConnectionPermission GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(ConnectionPermission entity)
    {
        throw new NotImplementedException();
    }

    public void Update(ConnectionPermission entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}