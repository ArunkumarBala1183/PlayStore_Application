namespace Playstore.Contracts.Data.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        int Count();
    }
}