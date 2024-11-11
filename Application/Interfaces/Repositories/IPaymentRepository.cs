using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<Payment> Get(Guid id);
        Task<Payment>Get(Expression<Func<Payment, bool>> expression);
        Task<ICollection<Payment>> GetAllPayment();
    }
}
