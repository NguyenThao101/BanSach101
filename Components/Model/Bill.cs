namespace BanSach.Components.Model
{
    public class Bill
    {
        public int BillId { get; set; }
        public string? OrderCode { get; set; }
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

        public int WarehouseID { get; set; }


    }
}
