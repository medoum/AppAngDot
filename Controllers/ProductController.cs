using CrudApi.Models.DTOS;
using CrudApi.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            var productDtos = await _productService.GetAllAsync();

            return Ok(productDtos);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetDetailProduct(Guid id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Please Provide a valid Product Id");
                }
                var productDto = await _productService.GetByIdAsync(id);

                if (productDto == null)
                {
                    return NotFound($"No Product was found with the given Id {id}");
                }
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured ! Please try again later");
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                // Verify if a product with the same sku already exists
                if (await _productService.SkuExistsAsync(productDto.Sku))
                {
                    return Conflict("Product with the same SKU Already exists");
                }

                // add product
                var product = await _productService.AddAsync(productDto);

                // The product is successfully added, return the created product using get product details method
                return CreatedAtAction(nameof(GetDetailProduct), new { Id = product.Id }, product);
            }
            catch (Exception ex)
            {

                // Return Server error message
                return StatusCode(500, "An error occurred! Please try again later");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductDto productDto)
        {
            try
            {
                if (productDto.Id == null || id != productDto.Id)
                {
                    return BadRequest("Please Provide a valid Product Id");
                }

                await _productService.UpdateAsync(productDto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred! Please try again later");
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                // note that id = 0 means that the id was not provided (null)
                if (id == null)
                {
                    return BadRequest("Please provide an id of the product to delete");
                }

                var product = await _productService.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound("The requested product to delete was not found");
                }
                await _productService.RemoveAsync(id);
                return Ok(new { message = "Product was successfully Deleted" });
            }
            catch
            {
                return StatusCode(500, "An error occurred! Please try again later");
            }
        }
    }
}
