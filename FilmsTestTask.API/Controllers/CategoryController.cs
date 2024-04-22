using FilmsTestTask.APIServices.Interfaces;
using FilmsTestTask.APIServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmsTestTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IBaseCrudService<CategoryApiModel> _categoryCrudService;
        private readonly ICategoryService _categoryService;

        public CategoryController(IBaseCrudService<CategoryApiModel> categoryCrudService, ICategoryService categoryService)
        {
            _categoryCrudService = categoryCrudService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryCrudService.GetAllAsync();
                if (categories.Count == 0)
                {
                    return NotFound("Categories not found");
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _categoryCrudService.GetAsync(id);
                if (category is null)
                {
                    return NotFound("Category not found");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("layer/{id}")]
        public async Task<IActionResult> GetCategoryLayer(int id)
        {
            try
            {
                var category = await _categoryCrudService.GetAsync(id);
                if (category is null)
                {
                    return NotFound("Category not found");
                }

                var layer = await _categoryService.CountLayerAsync(id);
                return Ok(layer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryApiModel model)
        {
            await _categoryCrudService.AddAsync(model);

            return Ok("Category added");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryApiModel model)
        {
            try
            {
                await _categoryService.UpdateAsync(model);
                return Ok("Category updated");
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);

            return Ok("Category deleted");
        }
    }
}
