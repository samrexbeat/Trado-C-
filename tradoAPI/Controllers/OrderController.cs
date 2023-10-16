using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tradoAPI.Data;
using tradoAPI.Models;

namespace tradoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrderController : Controller
    {
        private readonly ProductDbContext productDbContext;
        public OrderController(ProductDbContext productDbContext)
        {
            this.productDbContext = productDbContext;
        }

        //Get all orders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var products = await productDbContext.Orders.ToListAsync();
            return Ok(products);

        }

        //Get single order
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetOrder")]

        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            var product = await productDbContext.Orders.FirstOrDefaultAsync(x => x.id == id);
            if (product != null)
            {
                return Ok(product);

            }
            return NotFound("Order not found");

        }

        //Add order
        [HttpPost]

        public async Task<IActionResult> AddOrder([FromBody] Orders orders)
        {
            orders.id = Guid.NewGuid();
            await productDbContext.Orders.AddAsync(orders);
            await productDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = orders.id }, orders);


        }

        //Delete a Order
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            var existingOrder = await productDbContext.Orders.FirstOrDefaultAsync(x => x.id == id);
            if (existingOrder != null)
            {
                productDbContext.Orders.Remove(existingOrder);
                await productDbContext.SaveChangesAsync();
                return Ok(existingOrder);
            }
            return NotFound("Product not found");

        }
    }
}
