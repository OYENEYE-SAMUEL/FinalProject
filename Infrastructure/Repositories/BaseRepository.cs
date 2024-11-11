using Application.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected WasteContext _wasteContext;
        public async Task<bool> Check(Expression<Func<T, bool>> expression)
        {
            var exist = await _wasteContext.Set<T>().AnyAsync(expression);
            return exist;
        }

        public async Task<T> Create(T entity)
        {
            await _wasteContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _wasteContext.Set<T>().Update(entity);
            return entity;
        }
    }
}
