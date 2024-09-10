using Microsoft.AspNetCore.Mvc;
using ThucChienEFCore.Models;
using ThucChienEFCore.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ThucChienEFCore.Controllers
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

        // GET: api/category
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound("Danh mục không tồn tại");
            }
            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.AddAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        // PUT: api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            /*  if (id != category.Id)
              {
                  return BadRequest("Id không hợp lệ");
              }

              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }

              var existingCategory = await _categoryService.GetByIdAsync(id);
              if (existingCategory == null)
              {
                  return NotFound("Danh mục không tồn tại");
              }

              await _categoryService.UpdateAsync(category);
              return NoContent();*/

            if (id != category.Id)
            {
                return BadRequest("Id danh mục không hợp lệ");
            }

            try
            {
                await _categoryService.UpdateAsync(category);
                return NoContent();
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound("Danh mục không tồn tại");
            }

            await _categoryService.RemoveAsync(id);
            return NoContent();
        }
    }
}
