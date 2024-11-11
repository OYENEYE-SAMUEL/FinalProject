using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Repositories
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _context;
        public CurrentUser(IHttpContextAccessor context)
        {
            _context = context; 
        }
        public string GetCurrentUser()
        {
            try
            {
                var httpContext = _context.HttpContext;
                var emailClaim = httpContext.User.FindFirst(ClaimTypes.Email);

                return emailClaim.Value;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Email claim not found.");
            }
        }
    }
}
