using E_commerce.Dto;
using E_commerce.Models;
using E_commerce.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserViewController : ControllerBase
    {
        private IAdminUserViewService _Services;
        public UserViewController(IAdminUserViewService services)
        {
            _Services = services;
        }
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _Services.AllUser();
                return Ok(users);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}/admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _Services.GetUserById(id);
            if (user == null)
                return NotFound(new ApiResponse<string>(404, "User not found", null));

            var res = new ApiResponse<UserViewDto>(200, "Fetched user by id", user);
            return Ok(res);
        }
        [HttpPatch("{id}/blockunblock")]
        [Authorize("Admin")]
        public async Task<IActionResult> BlockorUnblock(int id)
        {
            try
            {
                bool isblocked = await _Services.Blockandunblock(id);
                return Ok(isblocked);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
