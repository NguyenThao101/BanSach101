namespace BanSach.Components.Model
{
    public class Product_bill
    {
        public int ProductBillId { get; set; }
        public int BillId { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }

    // Enum trạng thái đơn hàng
    public enum OrderStatus
    {
        Pending,     // Đang chờ xử lý
        Processing,  // Đang xử lý
        AwaitingPickup, // chờ lấy hàng
        Delivering,  // Đang giao
        Completed,   // Đã hoàn tất
        Cancelled    // Đã hủy
    }
}
