using BanSach.Components.Data;
using BanSach.Components.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanSach.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBillController : ControllerBase
    {
        private readonly AppDbContext context;

        public ProductBillController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product_bill>> GetProductBill(int id)
        {
            var productBill = await context.Product_bills.FindAsync(id);
            if (productBill == null)
            {
                return NotFound();
            }
            return productBill;
        }
    }
}
