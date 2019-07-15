namespace AP.Web.Persistence.Data.Entities
{
    public class Agency
    {
        public int AgencyId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }

        public virtual User AgencyManager { get; set; }
    }
}
