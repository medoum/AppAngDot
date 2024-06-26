using CrudApi.Models.Domain;
using CrudApi.Models.DTOS;
using CrudApi.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()

        {
            var categoryDtos = await _categoryService.GetAllAsync();

            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetDetailCategory(Guid id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Please Provide a valid Category Id");
                }
                var categoryDto = await _categoryService.GetByIdAsync(id);

                if (categoryDto == null)
                {
                    return NotFound($"No Category was found with the given Id {id}");
                }
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured ! Please try again later");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {

                if (await _categoryService.CategoryExists(categoryDto.Id))
                {
                    return Conflict("Category Already exists");
                }

                // add category
                var category = await _categoryService.AddAsync(categoryDto);

                // The category is successfully added, return the created category using get category details method
                return CreatedAtAction(nameof(GetDetailCategory), new { Id = category.Id }, category);
            }
            catch (Exception ex)
            {

                // Return Server error message
                return StatusCode(500, "An error occurred! Please try again later");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryDto categoryDto)
        {
            try
            {
                if (categoryDto.Id == null || id != categoryDto.Id)
                {
                    return BadRequest("Please Provide a valid Category Id");
                }

                await _categoryService.UpdateAsync(categoryDto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred! Please try again later");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                // note that id is null means that the id was not provided (null)
                if (id == null)
                {
                    return BadRequest("Please provide an id of the category to delete");
                }

                var category = await _categoryService.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound("The requested category to delete was not found");
                }

                await _categoryService.RemoveAsync(id);
                return Ok("category was successfully Deleted");
            }
            catch
            {
                return StatusCode(500, "An error occurred! Please try again later");
            }
        }

    }
}
