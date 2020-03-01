namespace ChatApi.Domain.Services
{
    public interface IRepository<T>
    {
        T[] Get();

        T Get(object id);

        T Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
