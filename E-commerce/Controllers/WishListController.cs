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
    public class WishListController : ControllerBase
    {
        private readonly IWishListService _service;
        public WishListController(IWishListService service)
        {
            _service = service;
        }
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> AddtoWishlist(int productid)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            string result = await _service.AddToWishList(userId, productid);

            if (result == "Item added to wish list.")
            {
                return Ok("The product is added to wishlist");
            }
            else if (result == "Item already in the wishlist.")
            {
                return Ok("Item is already in the wishlist");
            }

            return BadRequest(result); 
        }


        [HttpGet]
        [Authorize (Roles ="user")]
        public async Task<IActionResult> GetWhishLists()
        {
            try
            {

                int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
                var res = await _service.GetWishList(userId);

                return Ok(new ApiResponse<List<WishListViewDto>>(200, "Whishlist fetched successfully", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, "Failed to fetch wishlist", null, ex.Message));
            }
        }
        [HttpDelete]
        [Authorize(Roles ="user")]
        public async Task<IActionResult> RemovefromWishlist(int productid)
        {
            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            bool isadded = await _service.RemoveFromWishlist(userId, productid);
            if (isadded)
            {
                return Ok("The product is removed from wish list");
            }
            else
            {
                return BadRequest("Item not found in the wish list");
            }
        }

    }
}
