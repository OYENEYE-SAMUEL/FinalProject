using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IStaffRepository : IBaseRepository<Staff>
    {
        Task<Staff?> Get(Guid id);
        Task<Staff?> Get(Expression<Func<Staff, bool>> expression);
        Task<ICollection<Staff?>> GetAll();

    }
}
