namespace AP.Web.Services.Models.Dtos
{
    public class ProductsDto
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public decimal Price { get; set; }
    }
}
