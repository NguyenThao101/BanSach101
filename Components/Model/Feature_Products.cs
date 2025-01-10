namespace BanSach.Components.Model
{
    public class Feature_Products
    {
        public int FeaturePId { get; set; } 
        public int ProductId { get; set; }
        public string? FeatureName { get; set; }    
        public string? CostPrice { get; set; }
        public string? SellPrice { get; set; }
        public int Quantity {  get; set; }
        public string? Img { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}
