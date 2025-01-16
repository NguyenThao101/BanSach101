using BanSach.Components.Model;
using BanSach.Components.Model.ViewModel;

namespace BanSach.Components.IService
{
    public interface IBillService
    {
        Task<Bill> GetBillById(int BillId);
        Task<List<BillVanDonDTO>> GetBillByDay(DateTime? ToDate, DateTime? FromDate);
        Task<List<BillVanDonDTO>> GetBill();
        Task<List<BillVanDonDTO>> GetBillBySatus(string status);
        Task<List<Product_bill>> GetAllProduct_bill();
        Task DeleteProductFromBill(int billId, int productId);
        Task DeleteAllProductsFromBill(int billId);
        Task EditProduct_bill(Product_bill Product_bill);
        Task EditBill(Bill bill);
        Task Deletebill(Bill bill);
        Task<List<Bill>> GetAllbill();
        Task<(decimal doanhThu, int doanhSo, int soDon)> GetDoanhThuThangHienTai(DateTime currentDate);
        Task<Delivery?> GetDeById(int DeId);
        Task<List<Delivery>> GetAllDelivery();
        Task<bool> CompleteProductBillAsync(BillVanDonDTO bill);
        Task<List<DoanhThuViewModel>> GetAllBillDoanhThu(DateTime? fromDate, DateTime? toDate);
        Task<PagedResult<BillVanDonDTO>> GetBillHoanthanh(int page, int pageSize);
        Task<PagedResult<BillVanDonDTO>> GetBillCho(int page, int pageSize);
        Task<PagedResult<BillVanDonDTO>> GetBillDanggiao(int page, int pageSize);
        Task<PagedResult<BillVanDonDTO>> GetBillLayhang(int page, int pageSize);
        Task<List<TopProductViewModel>> GetTopProducts(DateTime? fromDate, DateTime? toDate);
        Task<bool> ApproveProductBillAsync(BillVanDonDTO bill);
        Task<bool> DeleteProductBillAsync(BillVanDonDTO bill);
        Task<List<ProductBillDetailDto>> GetProductsByBillId(int billId);
        Task<Address> GetAddressUser(int UserID);
        Task<(Delivery, int code, string message)> SetDeliveryBill(int? BillID, int DeliveryID, int WarehouseID);

        Task<(Bill, int code, string message)> ConfirmOrder(Bill model, string type, ShippingInfoDTO address);

        Task<List<Warehouse>> GetAllWarehouse();

        Task<List<ProductBillDetailDto>> GetProductsByBill();
        Task<TopProductViewModel> GetTotalQuantityP(int productId);

        //    Task<(decimal doanhThu, int doanhSo, int soDon)> GetDoanhThuTheoTrangThai(DateTime currentDate, string? orderStatus = null);

    }
}
