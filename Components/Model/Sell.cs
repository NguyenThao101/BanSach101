namespace BanSach.Components.Model
{
    public class Sell
    {
        public int SellId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
