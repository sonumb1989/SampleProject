using System.Collections.Generic;

namespace AP.Web.Persistence.Data.Entities
{
    public class Size : BaseEntity
    {
        public int SizeId { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
