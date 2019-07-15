namespace AP.Web.Persistence.Data.Entities
{
    public class Setting : BaseEntity
    {
        public int SettingId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int? TypeId { get; set; }
    }
}
