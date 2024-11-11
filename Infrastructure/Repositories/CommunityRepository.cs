using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class CommunityRepository : BaseRepository<Community>, ICommunityRepository
    {
        public CommunityRepository(WasteContext wasteContext)
        {
            _wasteContext = wasteContext;
        }
        public async Task<Community?> Get(Guid id)
        {
            var waste = await _wasteContext.Communities
                .Include(a => a.WasteCollections)
                .ThenInclude(x => x.WasteReport)
                .Include(s => s.Subscription)
                .FirstOrDefaultAsync(s => s.Id == id && s.IsActive.Equals(true));
            return waste;
        }

        public async Task<Community?> Get(Expression<Func<Community, bool>> expression)
        {
            var collection = await _wasteContext.Communities
                    .Include(a => a.WasteCollections)
                    .ThenInclude(x => x.WasteReport)
                    .Include(s => s.Subscription)
                    .FirstOrDefaultAsync(expression);
            return collection;

        }

        public async Task<ICollection<Community?>> GetAll()
        {
            var collection = await _wasteContext.Communities
                     .Include(a => a.WasteCollections)
                     .ThenInclude(x => x.WasteReport)
                     .Include(s => s.Subscription)
                     .ToListAsync();
            return collection;
        }
    }
}


