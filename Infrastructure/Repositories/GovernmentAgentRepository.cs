using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GovernmentAgentRepository : BaseRepository<GovernmentAgent>, IGovernmentAgentRepository
    {
        public GovernmentAgentRepository(WasteContext wasteContext)
        {
            _wasteContext = wasteContext;
        }
        public async Task<GovernmentAgent?> Get(Guid id)
        {
            var govt = await _wasteContext.GovernmentAgents
                .Include(x => x.Contract)
                .Include(r => r.WasteCollections)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive.Equals(true));
            return govt;
        }

        public async Task<GovernmentAgent?> Get(Expression<Func<GovernmentAgent, bool>> expression)
        {
            var govt = await _wasteContext.GovernmentAgents
                 .Include(x => x.Contract)
                .Include(r => r.WasteCollections)
                .FirstOrDefaultAsync(expression);
            return govt;
        }

        public async Task<ICollection<GovernmentAgent?>> GetAll()
        {
            var govts = await _wasteContext.GovernmentAgents
                .Include(x => x.Contract)
                .Include(r => r.WasteCollections)
                .ToListAsync();
            return govts;
        }
    }
}
