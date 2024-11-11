using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(WasteContext wasteContext) 
        {
            _wasteContext = wasteContext;
        }
        public async Task<Role?> GetRole(string name)
        {
            var role = await _wasteContext.Roles
                .Include(a => a.Users)
                .FirstOrDefaultAsync(x => x.Name == name && x.IsActive == true);
            return role;
        }

        public async Task<Role?> Get(Expression<Func<Role, bool>> expression)
        {
            var role = await _wasteContext.Roles
                .Include(a => a.Users)
                .FirstOrDefaultAsync(expression);
            return role;
        }

        public async Task<ICollection<Role?>> GetAll()
        {
            var roles = await _wasteContext.Roles
                .Include(a => a.Users)
                .ToListAsync();
            return roles;
        }
    }
}
