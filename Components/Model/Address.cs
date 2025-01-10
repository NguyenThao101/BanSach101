namespace BanSach.Components.Model
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string? DetailAddress { get; set; }
        public string? WardId { get; set; }
        public string? Phone { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
