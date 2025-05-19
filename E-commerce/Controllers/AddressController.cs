using E_commerce.Dto;
using E_commerce.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _services;
        public AddressController(IAddressService services)
        {
            _services = services;
        }
        [HttpPost]
        [Authorize(Roles ="user")]
        public async Task<IActionResult> CreateAddress([FromForm] CreateAddressDto address)
        {
            var userId = Convert.ToInt32(HttpContext.Items["UserId"]);

            var res = await _services.AddAdress(userId, address);

            if (res.StatusCode == 400)
            {
                return BadRequest(" Already 3 Addresses of delivery Exist,Update it ");
            }
            return Ok(res);

        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> ShowAddresses()
        {
            var userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            var addresses = await _services.ShowAddresses(userId);
            return Ok(addresses);
        }
        [HttpDelete("{Addressid}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteAddress(int Addressid)
        {
            var userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            bool isaddress = await _services.DeleteAddress(userId, Addressid);
            if (isaddress)
            {
                return Ok("Deleted Successfully");
            }
            return NotFound("there is no address in this address id");
        }
    }
}
