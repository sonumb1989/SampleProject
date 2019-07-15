using System;

namespace AP.Web.Persistence.Data.Entities
{
    public class Subscribe : BaseEntity
    {
        public int SubscribeId { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
