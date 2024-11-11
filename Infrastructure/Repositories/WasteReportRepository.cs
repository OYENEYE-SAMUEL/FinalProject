using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class WasteReportRepository : BaseRepository<WasteReport>, IWasteReportRepository
    {
        public WasteReportRepository(WasteContext wasteContext)
        {
            _wasteContext = wasteContext;
        }
        public async Task<WasteReport?> Get(Guid id)
        {
            var report = await _wasteContext.WasteReports
                .FirstOrDefaultAsync(r => r.Id == id && r.IsActive.Equals(true));
            return report;

        }

        public async Task<WasteReport?> Get(Expression<Func<WasteReport, bool>> expression)
        {
            var report = await _wasteContext.WasteReports
                .FirstOrDefaultAsync(expression);
            return report;
        }

        public async Task<ICollection<WasteReport?>> GetAll()
        {
            var report = await _wasteContext.WasteReports
                .ToListAsync();
            return report;
        }
    }
}
