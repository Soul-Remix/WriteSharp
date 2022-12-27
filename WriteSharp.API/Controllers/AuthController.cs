using WriteSharp.API.DTO.Auth;
using WriteSharp.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WriteSharp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/Auth
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model)
    {
        try
        {
            await _authService.RegisterUserAsync(model);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto model)
    {
        try
        {
            var result = await _authService.LoginUserAsync(model);
            return Ok(result);
        }
        catch
        {
            return BadRequest();
        }
    }
}