using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using tradoAPI.Data;
using tradoAPI.Models;
using tradoAPI.Models.DTO;
using tradoAPI.Repo.Abstract;
using Microsoft.AspNetCore.Hosting;

namespace tradoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductDbContext productDbContext;
        private readonly IFileService _fileService;
        private readonly IproductRepo _productRepo;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductsController(ProductDbContext productDbContext, IFileService fileService, IproductRepo productRepo, IWebHostEnvironment hostEnvironment)
        {
            this.productDbContext = productDbContext;
            this._fileService = fileService;
            this._productRepo = productRepo;
            this.hostEnvironment = hostEnvironment;
        }






        //Get all products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productDbContext.Products
                 .Select(x => new Products()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     PointValue = x.PointValue,
                     Price = x.Price,
                     ImageUrl = x.ImageUrl,
                     ImagePath = String.Format("{0}://{1}{2}/Uploads/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageUrl)
                 })
                 .ToListAsync();
            return Ok(products);

        }

        //Get single product
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetProduct")]

        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            var product = await productDbContext.Products
                 .Select(x => new Products()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     PointValue = x.PointValue,
                     Price = x.Price,
                     ImageUrl = x.ImageUrl,
                     ImagePath = String.Format("{0}://{1}{2}/Uploads/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageUrl)
                 })
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                return Ok(product);

            }
            return NotFound("Product not found");

        }

        //Add product
        [HttpPost]

        public async Task<IActionResult> AddProduct([FromForm] Products products)
        {
            products.Id = Guid.NewGuid();
            products.Created= DateTime.Now;
            products.ImageUrl = await SaveImage(products.ImageFile);
            await productDbContext.Products.AddAsync(products);
            await productDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = products.Id }, products);


        }



        //Updating a product
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UptdateProduct([FromRoute] Guid id, [FromBody] Products products)
        {
            var existingProduct = await productDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct != null)
            {
                existingProduct.Name = products.Name;
                existingProduct.Description = products.Description;
                existingProduct.Price = products.Price;
                existingProduct.PointValue = products.PointValue;
                existingProduct.ImageUrl = products.ImageUrl;
                existingProduct.Status= products.Status;
                await productDbContext.SaveChangesAsync();
                return Ok(existingProduct);
            }
            return NotFound("Product not found");
        }


        //Delete a product
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var existingProduct = await productDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct != null)
            {
                productDbContext.Products.Remove(existingProduct);
                await productDbContext.SaveChangesAsync();
                return Ok(existingProduct);
            }
            return NotFound("Product not found");
        }

        [NonAction]

        public async Task <string> SaveImage(IFormFile ImageFile)
        {
            string ImageUrl = new String(Path.GetFileNameWithoutExtension(ImageFile.FileName).Take(10).ToArray()).Replace(" ","-");
            ImageUrl = ImageUrl + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(ImageFile.FileName);
            var fileWithPath =  Path.Combine(hostEnvironment.ContentRootPath, "Uploads", ImageUrl);
            using (var stream = new FileStream(fileWithPath, FileMode.Create))
            {
              await ImageFile.CopyToAsync(stream);
            } ;
            return ImageUrl;

            //[HttpPost]
            //[Route("add")]
            //public async Task<IActionResult> Add([FromForm] Products model)
            /// <summary>
            //{
            /// </summary>
            ///////
            ///var status = new Status();
            //if (!ModelState.IsValid)
            //{
            // status.StatusCode = 0;
            //status.StatusMessage = "Pass the valid data";
            //return Ok(status);
            //}
            //if (model.ImageFile != null)
            //{
            //  var fileResult = _fileService.SaveImage(model.ImageFile);
            //if (fileResult.Item1 == 1)
            //{
            //  model.ImageUrl = fileResult.Item2;
            //}
            //var productResult = _productRepo.Add(model);
            //if (productResult)
            //{
            /*  status.StatusCode = 1;
              status.StatusMessage = "Image Added";
          }
          else
          {
              status.StatusCode = 0;
              status.StatusMessage = "Error on adding image";
          }

      }
      return Ok(status);
  }*/
        }

    } }
