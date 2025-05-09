using E_commerce.Dto;
using E_commerce.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _services;

        public CategoryController(ICategoryService services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewDto categoryViewDto)
        {
            try
            {
                var res = await _services.AddCategory(categoryViewDto);
                if (res)
                {
                    return Ok("Category  added");
                }
                return Conflict(" Category already exist");

            }
            catch (Exception ex)
            {
                return BadRequest(new Exception(ex.Message));
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCategory()
        {
            try
            {
                var categories = await _services.ViewCategory();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var res = await _services.RemoveCategory(id);
                if (res)
                {
                    return Ok("Category deleted successfully");
                }
                return NotFound("Category Not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
