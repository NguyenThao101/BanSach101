namespace BanSach.Components.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public int Year { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public int CostPrice { get; set; }
        public string? Description { get; set; }
        public int SellPrice { get; set; }
        public int Quantity { get; set; }
        public int PageNumber { get; set; }
        public string? Img { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

    }
}
