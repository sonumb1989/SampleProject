using System;

namespace AP.Web.Persistence.Data.Entities
{
    public class StockHistory : BaseEntity
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
