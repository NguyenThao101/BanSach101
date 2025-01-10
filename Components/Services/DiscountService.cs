using BanSach.Components.Data;
using BanSach.Components.IService;
using BanSach.Components.Model;
using Microsoft.EntityFrameworkCore;

namespace BanSach.Components.Services
{
    public class DiscountService : IDiscountServicecs
    {
        private readonly AppDbContext db;
        public DiscountService(AppDbContext _db)
        {
            db = _db;
        }
        public async Task<List<Discount>> GetAllItem()
        {
            return await db.Discount.OrderByDescending(i => i.DiscountId).ToListAsync();
        }
        public async Task<List<Discount>> GetAllDiscountsTodayAsync()
        {
            // Lấy ngày hôm nay
            DateTime today = DateTime.Today;

            // Truy vấn danh sách khuyến mãi có StartDate <= ngày hôm nay và EndDate >= ngày hôm nay
            var discountsToday = await db.Discount
                .Where(d => d.StartDate <= today && d.EndDate >= today)
                .ToListAsync();

            return discountsToday;
        }
        public async Task<Discount> CreateItem(Discount Discount)
        {
            // Thêm Discount vào cơ sở dữ liệu
            db.Discount.Add(Discount);
            await db.SaveChangesAsync();
            return Discount; // Trả về đối tượng Discount đã thêm
        }

        public async Task DeleteItem(Discount Discount)
        {
            db.Discount.Remove(Discount);
            await db.SaveChangesAsync();
        }

        public async Task EditItem(Discount Discount)
        {
            db.Discount.Update(Discount);
            await db.SaveChangesAsync();
        }
        public async Task<Discount?> GetItemById(int DiscountId)
        {
            var Discount = await db.Discount.FirstOrDefaultAsync(p => p.DiscountId == DiscountId);
            if (Discount == null)
            {
                return null;
            }

            return Discount;
        }
    }
}
