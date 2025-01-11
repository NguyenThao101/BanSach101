using BanSach.Components.Data;
using BanSach.Components.Model;
using Microsoft.EntityFrameworkCore;
namespace BanSach.Components.Services.ReviewService
{

    public class ReviewService
    {
        private readonly AppDbContext _context;


        public ReviewService(AppDbContext context)
        {
            _context = context;
        }


        public async Task SaveReview(Review review)
        {
            Console.WriteLine($"Saving Review: Rating={review.Rating}, ReviewText={review.ReviewText}");

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> HasReviewedOrderAsync(int userId,int productBillId)
        {
            return await _context.Reviews
                .AnyAsync(r => r.UserId == userId && r.ProductBillId == productBillId);
        }
        public async Task<List<Review>> GetReviewsForProductAsync( int productId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)  
                .ToListAsync();

            return reviews;
        }




    }

}