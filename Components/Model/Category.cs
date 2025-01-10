namespace BanSach.Components.Model
{
    public class Category
    {
        public int CategoryId { get; set; } 
        public string? CategoryName { get; set; }
        public string? Img {  get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}
