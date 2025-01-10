namespace BanSach.Components.Model
{
    public class Product_cart
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Product Product { get; set; }
        public bool IsCheckedOut { get; set; }
        public bool IsSelected { get; set; }
        public decimal Price { get; set; }
    }
}
