using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IContractRepository : IBaseRepository<Contract>
    {
        Task<Contract?> Get(Guid id);
        Task<Contract?> Get(Expression<Func<Contract, bool>> expression);
        Task<ICollection<Contract?>> GetAll();
    }
}
