namespace BanSach.Components.Model.ViewModel
{
    public class BillVanDonDTO
    {
        public int BillId { get; set; }
        public int UserID { get; set; }
        public int AddressId { get; set; }
        public int DeliveryId { get; set; }
        public string PayStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Note { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public string Status { get; set; }
        public bool ApproveBill { get; set; }
        public string UserName { get; set; }
        public int WarehouseID { get; set; }

    }
}
