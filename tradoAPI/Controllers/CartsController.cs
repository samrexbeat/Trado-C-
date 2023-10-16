using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using tradoAPI.Data;
using tradoAPI.Models;

namespace tradoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : Controller
    {
        private readonly ProductDbContext productDbContext;
        public CartsController(ProductDbContext productDbContext)
        {
            this.productDbContext = productDbContext;
        }

        //Get all products in the Cart
        [HttpGet]
        public async Task<IActionResult> GetAllInCart()
        {
            var products = await productDbContext.Carts.ToListAsync();
            return Ok(products);

        }

       

        //Add product to Cart
        [HttpPost]

        public async Task<IActionResult> AddToCart([FromBody] Carts carts)
        {
            
            await productDbContext.Carts.AddAsync(carts);
            await productDbContext.SaveChangesAsync();
            
            return Ok();
        }

        //Delete all products in Cart
        [HttpDelete]

        public async Task<IActionResult> removeFromCart()
        {
            var addedproducts = await productDbContext.Carts.ExecuteDeleteAsync();
            
                await productDbContext.SaveChangesAsync();
                
            return Ok(addedproducts);

        }

        //Delete a product in Cart
        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid Id)
        {
            var existingProduct = await productDbContext.Carts.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingProduct != null)
            {
               productDbContext.Carts.Remove(existingProduct);
                await productDbContext.SaveChangesAsync();
                return Ok(existingProduct);
            }
            return NotFound("Cart is empty");
        }
    }
}
