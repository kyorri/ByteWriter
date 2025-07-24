using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ByteWriter.Models
{
    public class User : IdentityUser
    {
        public string? Bio { get; set; }
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }

        public ICollection<User> Following { get; set; } = new List<User>();
    }
}
