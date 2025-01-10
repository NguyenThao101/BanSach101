namespace BanSach.Components.Model.ViewModel
{
    public class ProductBillDetailDto
    {
        public int ProductBillId { get; set; }
        public int BillId { get; set; }
        public decimal Price { get; set; }
        public int FeaturePId { get; set; }
        public int Quantity { get; set; }
        public int WarehouseID { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string PaymentStatus { get; set; }

        public Bill Bills { get; set; }
        public Product Product { get; set; }
    }
}
