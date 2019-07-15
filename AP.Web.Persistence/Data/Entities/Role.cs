using System.Collections.Generic;

namespace AP.Web.Persistence.Data.Entities
{
    public class Role : BaseEntity
    {
        public int RoleId { get; set; } // RoleId (Primary key)
        public string Name { get; set; } // Name (length: 50)

        // Reverse navigation

        public virtual ICollection<User> Users { get; set; } // Many to many mapping
    }
}
