using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public bool IsActive { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
