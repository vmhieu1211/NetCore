namespace WebApiApp.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);

    }
}