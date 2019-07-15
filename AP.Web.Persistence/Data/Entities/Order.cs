using System;
using System.Collections.Generic;

namespace AP.Web.Persistence.Data.Entities
{
    public class Order : BaseEntity
    {
        public int OrderId { get; set; } // Id (Primary key)
        public int BillingId { get; set; } // BillingId
        public int ShippingId { get; set; } // ShippingId
        public decimal Total { get; set; } // Total
        public DateTime OrderDate { get; set; } // OrderDate
        public string Status { get; set; } // Status
        public string Description { get; set; } // Description
        public string Currency { get; set; }
        public int? UserId { get; set; }
        public bool IsCoupon { get; set; }
        public bool IsActive { get; set; }
        public int? PaymentMethod { get; set; }

        // Reverse navigation

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } // OrderDetails.FK_dbo.OrderDetails_dbo.Orders_OrderId

        // Foreign keys
        public virtual User User { get; set; }
        public virtual OrderAddress Billing { get; set; } // FK_Orders_OrderAddresses
        public virtual OrderAddress Shipping { get; set; } // FK_Orders_OrderAddresses1
    }
}
