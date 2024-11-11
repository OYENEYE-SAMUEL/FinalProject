using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<bool> Check(Expression<Func<T, bool>> expression);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
    }
}
