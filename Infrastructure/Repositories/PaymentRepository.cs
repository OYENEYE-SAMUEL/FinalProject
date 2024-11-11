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
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(WasteContext wasteContext)
        {
            _wasteContext = wasteContext;
        }
        public async Task<Payment> Get(Guid id)
        {
            var payment = await _wasteContext.Payments
                .Include(x => x.Subscription)
                .ThenInclude(e => e.Communities)
                .Include(x => x.Subscription)
                .ThenInclude(e => e.Individuals)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive == false);
            return payment;

        }

        public async Task<Payment> Get(Expression<Func<Payment, bool>> expression)
        {
            var payment = await _wasteContext.Payments
                 .Include(x => x.Subscription)
                .ThenInclude(e => e.Communities)
                .Include(x => x.Subscription)
                .ThenInclude(e => e.Individuals)
                .FirstOrDefaultAsync(expression);
            return payment;
        }

        public async Task<ICollection<Payment>> GetAllPayment()
        {
            return await _wasteContext.Payments
                 .Include(x => x.Subscription)
                .ThenInclude(e => e.Communities)
                .Include(x => x.Subscription)
                .ThenInclude(e => e.Individuals)
                .ToListAsync();
        }
    }
}
