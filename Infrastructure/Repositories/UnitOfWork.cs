using Application.Interfaces.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WasteContext _wasteContext;
        public UnitOfWork(WasteContext wasteContext)
        {
            _wasteContext = wasteContext;
        }
        public async Task<int> Save()
        {
            return await _wasteContext.SaveChangesAsync();
        }

        
    }
}
