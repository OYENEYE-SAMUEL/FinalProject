using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IIndividualRepository : IBaseRepository<Individual>
    {
        Task<Individual?> Get(Guid id);
        Task<Individual?> Get(Expression<Func<Individual, bool>> expression);
        Task<ICollection<Individual?>> GetAll();
    }
}
