using System.Linq.Expressions;

namespace SimpleWebApi.IRepository
{
    public interface IRepositoryBase<T>
    {
        ValueTask<T> GetFirstWithTracking(Expression<Func<T, bool>> expression);
        ValueTask<T> GetFirstWithNoTracking(Expression<Func<T, bool>> expression);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}