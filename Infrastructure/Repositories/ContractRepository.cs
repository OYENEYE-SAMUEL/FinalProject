using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ContractRepository : BaseRepository<Contract>, IContractRepository
    {
        public ContractRepository(WasteContext wasteContext)
        {
            _wasteContext = wasteContext;
        }
        public async Task<Contract?> Get(Guid id)
        {
            var contract = await _wasteContext.Contracts
                .Include(x => x.GovernmentAgents)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive.Equals(true));
            return contract;
        }

        public async Task<Contract?> Get(Expression<Func<Contract, bool>> expression)
        {
            var contract = await _wasteContext.Contracts
                .Include(x => x.GovernmentAgents)
                .FirstOrDefaultAsync(expression);
            return contract;
        }

        public async Task<ICollection<Contract?>> GetAll()
        {
            var contract = await _wasteContext.Contracts
                .Include(x => x.GovernmentAgents)
                .ToListAsync();
            return contract;
        }
    }
}
