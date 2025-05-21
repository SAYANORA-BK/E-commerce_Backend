
using E_commerce.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _userService.Register(dto);
        if (result == true)
        {
            return Ok("User Registered SuccessFully");
        }
        else
        {
            return BadRequest("Email Already Exist");
        }
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var token = await _userService.Login(loginDto);
        if (token == null)
        {
            return NotFound("user not found");
        }
        return Ok(token);
    }

}
