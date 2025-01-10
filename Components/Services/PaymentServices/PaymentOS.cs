using BanSach.Components.Data;
using BanSach.Components.Model;
using BanSach.Components.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Net.payOS;
using Net.payOS.Types;

namespace BanSach.Components.Services.PaymentServices
{
    public class PaymentOS : IPaymentOS
    {
        private readonly IWebHostEnvironment _iHostingEnvironment;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public PaymentOS(IWebHostEnvironment iHostingEnvironment, AppDbContext context, IConfiguration configuration)
        {
            _iHostingEnvironment = iHostingEnvironment;
            _context = context;
            _configuration = configuration;
        }
        public async Task<CreatePaymentResult> CreatePaymentLink(List<ProductBillDetailDto> model)
        {
            var billOrderCode = await _context.Bill.FirstOrDefaultAsync(x => x.BillId == model.First().Bills.BillId);
            billOrderCode.OrderCode = GenerateRandomString(7);
            await _context.SaveChangesAsync();
            PayOS payOS = new PayOS(_configuration.GetSection("ClientID").Value, _configuration.GetSection("APIKey").Value, _configuration.GetSection("ChecksumKey").Value);
            List<ItemData> items = new List<ItemData>();

            foreach (var book in model)
            {
                ItemData item = new ItemData(book.Product.ProductName, book.Quantity, (int)book.Price);
                items.Add(item);

            }

            if (long.TryParse(billOrderCode.OrderCode, out long orderCode))
            {
                PaymentData paymentData = new PaymentData(orderCode, (int)model.First().Bills.TotalPrice, "Thanh toán hóa đơn " + model.First().Bills.BillId.ToString(), items, _configuration.GetSection("CancelUrl").Value, _configuration.GetSection("ReturnUrl").Value);
                CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
                return createPayment;
            }
            else
            {
                // Xử lý lỗi nếu OrderCode không thể chuyển sang long
                throw new InvalidOperationException("OrderCode không hợp lệ.");
            }
        }
        public string GenerateRandomString(int length)
        {
            const string characters = "0123456789";
            Random random = new Random();
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = characters[random.Next(characters.Length)];
            }

            return new string(result);
        }
        public async Task<Bill> PaymentSucsses(string orderCode)
        {
            if (string.IsNullOrEmpty(orderCode))
            {
                throw new ArgumentException("Lỗi", nameof(orderCode));
            }

            var order = await _context.Bill.FirstOrDefaultAsync(x => x.OrderCode == orderCode);
            if (order == null)
            {
                throw new InvalidOperationException($"Lỗi {orderCode} không tìm thấy");
            }

            order.Note = "Đã thanh toán thành công";
            order.Status = OrderStatus.Completed.ToString();
            order.Updated_at = DateTime.Now;

            try
            {
                // Lưu thay đổi
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Lỗi.", ex);
            }

            return order;
        }



    }
}
