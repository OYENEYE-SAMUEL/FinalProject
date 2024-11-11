using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface ISubscriptionRepository : IBaseRepository<Subscription>
    {
        Task<Subscription?> Get(Guid id);
        Task<Subscription?> Get(Expression<Func<Subscription, bool>> expression);
        Task<ICollection<Subscription?>> GetAll();
    }
}
