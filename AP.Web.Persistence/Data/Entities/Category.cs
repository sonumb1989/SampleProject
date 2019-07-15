using System.Collections.Generic;

namespace AP.Web.Persistence.Data.Entities
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; } // Id (Primary key)
        public int ParentId { get; set; } // ParentId
        public string Name { get; set; } // Name
        public bool IsActive { get; set; } // IsActive
        public string Image { get; set; } // Image (length: 100)

        // Reverse navigation

        public virtual ICollection<Product> Products { get; set; } // Products.FK_dbo.Products_dbo.Categories_CategoryId
    }
}
