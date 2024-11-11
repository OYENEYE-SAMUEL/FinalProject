using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserRequestModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class UserResponseModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public bool IsActive { get; set; }
        public Role Role { get; set; } = default!;
        public string Token { get; set; } = default!;
    }

    public class TokenDto
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
