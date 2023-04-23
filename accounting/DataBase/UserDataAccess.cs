using accounting.Model;

namespace accounting.DataBase;

public class UserDataAccess : IDataAccess<User>
{
    public IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetSomeLastRecords(int i)
    {
        throw new NotImplementedException();
    }

    public User GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(User entity)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}