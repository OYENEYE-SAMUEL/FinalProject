using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role?> GetRole(string name);
        Task<Role?> Get(Expression<Func<Role, bool>> expression);
        Task<ICollection<Role?>> GetAll();
    }
}
