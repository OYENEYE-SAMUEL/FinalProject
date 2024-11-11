using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IWasteCollectionRepository : IBaseRepository<WasteCollection>
    {
        Task<WasteCollection?> Get(Guid id);
        Task<WasteCollection?> Get(Expression<Func<WasteCollection, bool>> expression);
        Task<ICollection<WasteCollection?>> GetAll();
    }
}
