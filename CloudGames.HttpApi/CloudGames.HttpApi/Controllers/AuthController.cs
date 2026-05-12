using CloudGames.Application.Inputs;
using CloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudGames.HttpApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(
        [FromBody] LoginInput input
    )
    {
        var user = await _userService.ValidateLogin(input);

        if (user == null)
            return Unauthorized("E-mail ou senha inválidos.");

        var token = _authService.GenerateToken(user);

        return Ok(new { Token = token, User = user });
    }
}
