namespace BanSach.Components.Model.ViewModel
{
    public class DoanhThuViewModel
    {
        public int BillId { get; set; }
        public DateTime? BillCreatedAt { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
        public string PayStatus { get; set; }

        public List<TopProductViewModel> products { get; set; }
    }
}
