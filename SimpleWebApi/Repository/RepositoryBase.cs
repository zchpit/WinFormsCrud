using SimpleWebApi.IRepository;
using SimpleWebApi.Model;
using System.Data.Entity;
using System.Linq.Expressions;

namespace SimpleWebApi.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected SimpleDbContext repositoryContext { get; set; }
        public RepositoryBase(SimpleDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        public IQueryable<T> FindAll() => repositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            repositoryContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => repositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);

    }
}
