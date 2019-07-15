using System;
using System.Collections.Generic;

namespace AP.Web.Persistence.Data.Entities
{
    public class Stock : BaseEntity
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int Quantity { get; set; }
        public int RemainQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool InStock { get; set; }

        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
        public virtual Color Color { get; set; }

        public virtual ICollection<StockHistory> StockHistories { get; set; }
    }
}
