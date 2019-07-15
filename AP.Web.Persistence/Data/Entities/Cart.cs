using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.Web.Persistence.Data.Entities
{
    public class Cart : BaseEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public string CartId { get; set; } // CartId
        public int ProductId { get; set; } // ProductId
        public int Quantity { get; set; } // Quantity
        public string Size { get; set; } // Size
        public string Color { get; set; } // Color
        public DateTime CreatedDate { get; set; } // CreatedDate

        public virtual Product Product { get; set; } // FK_dbo.Carts_dbo.Products_ProductId
    }
}
