namespace accounting.Model
{
    public interface IDataAccess<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetSomeLastRecords(int i);
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
