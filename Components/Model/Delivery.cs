namespace BanSach.Components.Model
{
    public class Delivery
    {
        public int DeliveryId { get; set; } 
        public string? DeliveryName { get; set; }   
        public string? Address { get; set; }    
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Description {  get; set; }   
        public string? Img { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}
