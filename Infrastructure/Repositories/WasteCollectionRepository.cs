using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class WasteCollectionRepository : BaseRepository<WasteCollection>, IWasteCollectionRepository
    {
        public WasteCollectionRepository(WasteContext wasteContext)
        {
            _wasteContext = wasteContext;
        }
        public async Task<WasteCollection?> Get(Guid id)
        {
            var waste = await _wasteContext.WasteCollections
                .Include(r => r.GovernmentAgent)
                .Include(d => d.Community)
                .Include(f => f.Staff)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive.Equals(true));
            return waste;
        }

        public async Task<WasteCollection?> Get(Expression<Func<WasteCollection, bool>> expression)
        {
            var waste = await _wasteContext.WasteCollections
                .Include(r => r.GovernmentAgent)
                .Include(d => d.Community)
                .Include(f => f.Staff)
               .FirstOrDefaultAsync(expression);
            return waste;
        }

        public async Task<ICollection<WasteCollection?>> GetAll()
        {
            var waste = await _wasteContext.WasteCollections
                .Include(r => r.GovernmentAgent)
                .Include(d => d.Community)
                .Include(f => f.Staff)
                .ToListAsync();
            return waste;
        }
    }
}
