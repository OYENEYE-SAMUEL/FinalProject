using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(WasteContext wasteContext)
        {
            _wasteContext = wasteContext;
        }
        public async Task<User> Get(string email)
        {
            var user = await _wasteContext.Users
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Email == email && e.IsActive.Equals(true));
            return user;

        }

        public async Task<User> Get(Expression<Func<User, bool>> expression)
        {
            var user = await _wasteContext.Users
                .Include(e => e.Role)
                .FirstOrDefaultAsync(expression);
            return user;
        }

        public async Task<ICollection<User>> GetAll()
        {
            var users = await _wasteContext.Users
                .Include(e => e.Role)
                .ToListAsync();
            return users;
        }
    }
}
