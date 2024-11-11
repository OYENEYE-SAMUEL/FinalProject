using Application.DTOs;
using Application.Interfaces.Services;
using Application.Settings;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthToken _authToken;
        public AuthService(IOptions<AuthToken> authToken)
        {
            _authToken = authToken.Value ?? throw new ArgumentNullException(nameof(authToken), "Auth token configuration is missing");
        }
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FullName),
                new Claim("roles", user.Role.Name)
            };

            if (string.IsNullOrWhiteSpace(_authToken.Key))
            {
                throw new ArgumentNullException(nameof(_authToken.Key), "JWT secret key cannot be null or empty.");
            }

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authToken.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _authToken.Issuer,
                audience: _authToken.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_authToken.DurationInMinutes),
                signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
