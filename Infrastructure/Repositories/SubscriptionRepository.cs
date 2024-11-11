using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(WasteContext wasteContext)
        {
            _wasteContext = wasteContext;
        }

        public async Task<Subscription?> Get(Guid id)
        {
            var subscribe = await _wasteContext.Subscriptions
                .Include(e => e.Communities)
                .Include(r=> r.Individuals)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive.Equals(true));
            return subscribe;
        }

        public async Task<Subscription?> Get(Expression<Func<Subscription, bool>> expression)
        {
            var subscribe = await _wasteContext.Subscriptions
                .Include(e => e.Communities)
                .Include(r => r.Individuals)
                .FirstOrDefaultAsync(expression);
            return subscribe;
        }

        public async Task<ICollection<Subscription?>> GetAll()
        {
            var subscribe = await _wasteContext.Subscriptions
                .Include(e => e.Communities)
                .Include(r => r.Individuals)
                .ToListAsync();
            return subscribe;
        }
    }
}
