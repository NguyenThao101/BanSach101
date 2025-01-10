namespace BanSach.Components.Model
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public int ProductId { get; set; }
        public int SellPrice { get; set; }
        public double Rate {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 

    }
}
