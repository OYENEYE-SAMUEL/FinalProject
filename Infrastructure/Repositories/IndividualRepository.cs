using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class IndividualRepository : BaseRepository<Individual>, IIndividualRepository
    {
        public IndividualRepository(WasteContext wasteContext)
        {
            _wasteContext = wasteContext;
        }
        public async Task<Individual?> Get(Guid id)
        {
            var person = await _wasteContext.Individuals
                .Include(x => x.WasteCollections)
                .Include(e => e.WasteReports)
                .Include(e => e.Subscription)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive == true);
            return person;
        }

        public async Task<Individual?> Get(Expression<Func<Individual, bool>> expression)
        {
            var person = await _wasteContext.Individuals
               .Include(x => x.WasteCollections)
               .Include(e => e.WasteReports)
               .Include(e => e.Subscription)
               .FirstOrDefaultAsync(expression);
            return person;
        }

        public async Task<ICollection<Individual?>> GetAll()
        {
            var person = await _wasteContext.Individuals
               .Include(x => x.WasteCollections)
               .Include(e => e.WasteReports)
               .Include(e => e.Subscription)
               .ToListAsync();
            return person;
        }
    }
}
