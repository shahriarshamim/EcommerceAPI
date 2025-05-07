using EcommerceAPI.Data;
using EcommerceAPI.Model.DTOs;
using EcommerceAPI.Model.Entites;
using EcommerceAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    //localhost:5000/api/Product/
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            var productDto = new List<ProductReadDto>();
            foreach (var product in products)
            {
                productDto.Add(new ProductReadDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    CreateAt = product.CreateAt,
                    StockQuantity = product.StockQuantity
                });
            }
            return Ok(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCrateDto productCrateDto)
        {
            var productDomain = new Product
            {
                Name = productCrateDto.Name,
                Description = productCrateDto.Description,
                Price = productCrateDto.Price,
                StockQuantity = productCrateDto.StockQuantity,
                ImageUrl = productCrateDto.ImageUrl,
                CreateAt = DateTime.Now,
            };
            //_context.Products.Add(productDomain);
            //_context.SaveChanges();

            await _productRepository.AddAsync(productDomain);
            return Ok();
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            //var product = _context.Products.FirstOrDefault(p => p.Id == id);
            var product = _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            //product.Name = productUpdateDto.Name;
            //product.Description = productUpdateDto.Description;
            //product.Price = productUpdateDto.Price;
            //product.StockQuantity = productUpdateDto.StockQuantity;

            //_context.SaveChanges();
            //await _productRepository.UpdateAsync(product);
            return Ok();
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            //var product = _context.Products.FirstOrDefault(p => p.Id == id);
            var product=_productRepository.DeleteAsync(id);
            
            //if (product == null)
            //{
            //    return NotFound();
            //}
            //_context.Products.Remove(product);
            //_context.SaveChanges();
            return Ok();
        }
    }
}
