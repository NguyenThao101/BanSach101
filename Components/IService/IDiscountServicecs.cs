using BanSach.Components.Model;

namespace BanSach.Components.IService
{
    public interface IDiscountServicecs
    {
        Task<List<Discount>> GetAllItem();
        Task<List<Discount>> GetAllDiscountsTodayAsync();
        Task<Discount> CreateItem(Discount Discount);
        Task DeleteItem(Discount Discount);
        Task EditItem(Discount Discount);
        Task<Discount?> GetItemById(int DiscountId);

    }
}
