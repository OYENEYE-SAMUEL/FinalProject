using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface ICommunityRepository : IBaseRepository<Community>
    {
        Task<Community?> Get(Guid id);
        Task<Community?> Get(Expression<Func<Community, bool>> expression);
        Task<ICollection<Community?>> GetAll();
    }
}
