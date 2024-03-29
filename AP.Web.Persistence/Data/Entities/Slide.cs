namespace AP.Web.Persistence.Data.Entities
{
    public class Slide : BaseEntity
    {
        public int SlideId { get; set; } // Id (Primary key)
        public string Image { get; set; } // Image (length: 100)
        public string Name { get; set; } // Name (length: 100)
        public string Description { get; set; } // Description (length: 50)
        public int TypeId { get; set; }
    }
}
