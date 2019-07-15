using System.Collections.Generic;

namespace AP.Web.Persistence.Data.Entities
{
    public class Color : BaseEntity
    {
        public int ColorId { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string Value { get; set; } // Value

        // Reverse navigation
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
