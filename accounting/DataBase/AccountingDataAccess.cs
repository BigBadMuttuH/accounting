using accounting.Model;

namespace accounting.DataBase;

public class AccountingDataAccess : IDataAccess<Accounting>
{
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