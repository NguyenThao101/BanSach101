using BanSach.Components.Model;

namespace BanSach.Components.IService
{
    public interface ICartService
    {
        Task<List<Product_cart>> GetAllPCart();
        Task<List<Product_cart>> GetPCartUser(int UserID);
        Task<Product_cart> CreatePCart(Product_cart productCart);
        Task DeletePCart(Product_cart productCart);
        Task<(string message, string code, int billID)> PlaceProductBill(List<Product_bill> productBill, int userId, decimal totalPrice);
        //Task<Product_bill> PlaceProductBill(Bill productBill, int userId);

        Task<Product_bill> GetProductBillById(int productBillId);
        Task<PaymentResult> ProcessPayment(int productBillId, string paymentMethod);
        Task<int> SaveShippingAddress(Address address);
        Task<ServiceResponse<int>> CreateBill(int addressId, int userId, decimal totalPrice, string note);
        Task<ServiceResponse<bool>> LinkBillToProductBill(int billId, List<Product_bill> productBills);
        Task SaveAddress(Address address);
        Task<bool> UpdateBillAsync(Bill bill);
        Task<(Bill, int code, string message)> CreateBillMain(List<Product_cart> cart);
        Task<(Bill, int code, string message)> CreateBillNow(Product product, int userID, int quantity);
        Task DeleteItem(Bill bill);
    }

    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
