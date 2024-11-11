using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> Get(string email);
        Task<User> Get(Expression<Func<User, bool>> expression);
        Task<ICollection<User>> GetAll();
    }
}
