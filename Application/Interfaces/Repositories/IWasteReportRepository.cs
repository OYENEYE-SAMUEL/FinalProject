using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IWasteReportRepository : IBaseRepository<WasteReport>
    {
        Task<WasteReport?> Get(Guid id);
        Task<WasteReport?> Get(Expression<Func<WasteReport, bool>> expression);
        Task<ICollection<WasteReport?>> GetAll();
    }
}
