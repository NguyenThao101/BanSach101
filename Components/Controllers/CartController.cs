using BanSach.Components.IService;
using BanSach.Components.Model;
using Microsoft.AspNetCore.Mvc;

namespace BanSach.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartController(ICartService _cartService)
        {
            cartService = _cartService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Product_cart>>> GetAllPCart()
        {
            var carts = await cartService.GetAllPCart();
            return Ok(carts);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Product_cart>> CreatePCart([FromBody] Product_cart productCart)
        {
            var createdCart = await cartService.CreatePCart(productCart);
            return Ok(createdCart);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePCart([FromBody] Product_cart productCart)
        {
            await cartService.DeletePCart(productCart);
            return NoContent();
        }

        //[HttpPost("PlaceOrder")]
        //public async Task<ActionResult<Product_bill>> PlaceProductBill([FromBody] Product_bill productBill)
        //{
        //    var order = await cartService.PlaceProductBill(productBill);
        //    return Ok(order);
        //}

        [HttpPost("Payment")]
        public async Task<ActionResult<PaymentResult>> ProcessPayment(int productBillId, string paymentMethod)
        {
            var result = await cartService.ProcessPayment(productBillId, paymentMethod);
            return Ok(result);
        }
    }
}
