using E_commerce.Dbcontext;
using E_commerce.Dto;
using E_commerce.Models;
using E_commerce.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    public class CartController:ControllerBase
    {
        
        private readonly ICartService _service;
        public CartController(ICartService service)
        {
            
            _service = service;
        }
        [HttpPost("AddToCart")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> AddtoCart(int productid)
        {
            if (HttpContext.Items["UserId"] is not int userid)
            {
                return Unauthorized("Invalid or missing user information.");
            }

            var isAdded = await _service.AddToCart(productid, userid);
            if (isAdded.StatusCode == 200)
            {
                return Ok(isAdded);
            }
            else if (isAdded.StatusCode == 404)
            {
                return NotFound(new ApiResponse<string>(404, isAdded.Message));
            }
            else if (isAdded.StatusCode == 409)
            {
                return Conflict(new ApiResponse<string>(409, isAdded.Message));
            }
            return BadRequest(new ApiResponse<string>(400, "Bad request"));
        }
        [HttpGet("GetCart")]
        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            int userid = Convert.ToInt32(HttpContext.Items["UserId"]);
            var cartitems = await _service.GetCart(userid);
            if (cartitems.Count == 0)
            {
                return Ok(new ApiResponse<IEnumerable<CartViewDto>>(200, "Cart is empty", cartitems));
            }
            return Ok(new ApiResponse<IEnumerable<CartViewDto>>(200, "Cart successfully fetched", cartitems));
        }
        [HttpDelete("Delete/{productId}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> RemoveCart(int productId)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Items["UserId"]);

                bool res = await _service.RemoveFromCart(userId, productId);
                if (res == false)
                {
                    return BadRequest(new ApiResponse<string>(400, "Item is not found in cart", null));
                }
                return Ok(new ApiResponse<string>(200, "Item successfully deleted"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, "Internal server error", null, ex.Message));
            }
        }
        [HttpPut("DecrementQuantity/{prductid}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DecrementQuantity(int prductid)
        {
            try
            {
                int userid = Convert.ToInt32(HttpContext.Items["UserId"]);
                bool items = await _service.DecrementQuantity(userid, prductid);
                if (items == false)
                {
                    return BadRequest(new ApiResponse<string>(400, "Item not found in the cart", null, "Item not found in the cart"));
                }
                return Ok(new ApiResponse<string>(200, "Quantity Decreased"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("IncrementQuantity/{prductid}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> IncrementQuantity(int prductid)
        {
            try
            {
                int userid = Convert.ToInt32(HttpContext.Items["UserId"]);
                bool items = await _service.IncrementQuantity(userid, prductid);
                if (items == false)
                {
                    return BadRequest(new ApiResponse<string>(400, "Item not found in the cart", null, "Item not found in the cart"));
                }
                return Ok(new ApiResponse<string>(200, "Quantity Increased"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
