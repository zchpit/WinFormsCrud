using Microsoft.EntityFrameworkCore;
using SimpleWebApi.IRepository;
using SimpleWebApi.Model;
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
        public async ValueTask<T> GetFirstWithTracking(Expression<Func<T, bool>> expression) => await repositoryContext.Set<T>().FirstAsync(expression);
        public async ValueTask<T> GetFirstWithNoTracking(Expression<Func<T, bool>> expression) => await repositoryContext.Set<T>().AsNoTracking().FirstAsync(expression);

        public IQueryable<T> FindAll() => repositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            repositoryContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => repositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);
    }
}
