using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IGovernmentAgentRepository : IBaseRepository<GovernmentAgent>
    {
        Task<GovernmentAgent?> Get(Guid id);
        Task<GovernmentAgent?> Get(Expression<Func<GovernmentAgent, bool>> expression);
        Task<ICollection<GovernmentAgent?>> GetAll();
    }
}
