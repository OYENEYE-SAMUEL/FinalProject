using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        public StaffRepository(WasteContext wasteContext)
        {
            _wasteContext = wasteContext;
        }
        public async Task<Staff?> Get(Guid id)
        {
            var staff = await _wasteContext.Staffs
                .Include(e => e.WasteCollections)
                .FirstOrDefaultAsync(r  => r.Id == id && r.IsActive.Equals(true));
            return staff;
        }

        public async Task<Staff?> Get(Expression<Func<Staff, bool>> expression)
        {
            var staff = await _wasteContext.Staffs
                .Include(e => e.WasteCollections)
                .FirstOrDefaultAsync(expression);
            return staff;
        }

        public async Task<ICollection<Staff?>> GetAll()
        {
            var staff = await _wasteContext.Staffs
                .Include(e => e.WasteCollections)
                .ToListAsync();
            return staff;

        }
    }
}
