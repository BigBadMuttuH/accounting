using accounting.Model;
using Npgsql;

namespace accounting.DataBase;

public class AccountingDataAccess : IDataAccess<Accounting>
{
    private static readonly PgSqlConnection Conn = new();

    private readonly string _connectionString = Conn.ToString();

    public IEnumerable<Accounting> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Accounting> GetSomeLastRecords(int i)
    {
        throw new NotImplementedException();
    }

    public Accounting GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(Accounting entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Accounting entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}