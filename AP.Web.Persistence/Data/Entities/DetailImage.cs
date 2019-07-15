namespace AP.Web.Persistence.Data.Entities
{
    public class DetailImage : BaseEntity
    {
        public int DetailImageId { get; set; } // Id (Primary key)
        public string Image { get; set; } // Image (length: 100)
        public int ProductId { get; set; } // ProductId
        public int Order { get; set; } // Order
        public string ProductCode { get; set; } // ProductCode (length: 10)

        // Foreign keys

        public virtual Product Product { get; set; } // FK_dbo.DetailImages_dbo.Products_ProductId
    }
}
