using BanSach.Components.Data;
using BanSach.Components.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanSach.Components.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("search-product")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string searchQuery)
        {
            var results = await _context.Products
                                         .Where(p => p.ProductName.Contains(searchQuery) ||
                                                     p.Author.Contains(searchQuery) ||
                                                     p.Publisher.Contains(searchQuery))
                                         .ToListAsync();

            return Ok(results);
        }
    }
}
