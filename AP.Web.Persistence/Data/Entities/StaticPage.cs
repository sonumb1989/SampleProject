using System;

namespace AP.Web.Persistence.Data.Entities
{
    public class StaticPage : BaseEntity
    {
        public int PageId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public bool Status { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public virtual User User { get; set; }
    }
}
