using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RoleRequestModel
    {
        public string Name { get; set; } = default!;
    }

    public class RoleResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<User> Users { get; set; } = new HashSet<User>();
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
