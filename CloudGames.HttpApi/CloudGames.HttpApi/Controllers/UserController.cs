using Asp.Versioning;
using CloudGames.Application.Inputs;
using CloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudGames.HttpApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromBody] UserCreateInput input
    )
    {
        var user = await _userService.CreateUser(input);

        return Ok(user);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetUserById(
        [FromRoute] Guid id
    )
    {
        var user = await _userService.GetUserById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();

        if (users == null)
            return NotFound();

        return Ok(users);
    }

    [HttpPatch]
    [Route("{userId}/game/{gameId}")]
    public async Task<IActionResult> AddGameByUser(
        [FromRoute] Guid userId,
        [FromRoute] Guid gameId
    )
    {
        var user = await _userService.AddGameByUser(userId, gameId);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPatch]
    [Route("reset-password")]
    public async Task<IActionResult> UpdatePasswordByUser(
        [FromBody] UserPasswordUpdateInput input
    )
    {
        var user = await _userService.UpdatePasswordByUser(input);

        if (user == null)
            return BadRequest();

        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(
        [FromBody] UserUpdateInput input
    )
    {
        var user = await _userService.UpdateUser(input);

        if (user == null)
            return BadRequest();

        return Ok(user);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteUser(
        [FromRoute] Guid id
    )
    {
        await _userService.DeleteUser(id);

        return NoContent();
    }
}
