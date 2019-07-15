namespace AP.Web.Persistence.Data.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderDetailId { get; set; } // Id (Primary key)
        public int OrderId { get; set; } // OrderId
        public int? ProductId { get; set; } // ProductId
        public string Size { get; set; } // Size
        public string Color { get; set; } // Color
        public int Quantity { get; set; } // Quantity
        public decimal UnitPrice { get; set; } // UnitPrice
        public string Status { get; set; } // Status

        // Foreign keys

        public virtual Order Order { get; set; } // FK_OrderDetails_Orders
        public virtual Product Product { get; set; } // FK_OrderDetails_Products
    }
}
