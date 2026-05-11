using Asp.Versioning;
using CloudGames.Application.Interfaces;
using CloudGames.Domain.Entities;
using CloudGames.Domain.Inputs;
using CloudGames.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CloudGames.HttpApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserService _userService;

    public UserController(IUserRepository userRepository, IGameRepository gameRepository)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromBody] UserCreateInput input
    )
    {
        try
        {
            var user = await _userService.CreateUserService(input);

            _userRepository.Create(user);

            return Created();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetUserById(
        [FromRoute] Guid id
    )
    {
        try
        {
            var user = _userRepository.GetById(id);

            return Ok(user);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = _userRepository.GetAll().ToList();

            return Ok(users);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPatch]
    [Route("{userId}/game/{gameId}")]
    public async Task<IActionResult> AddGameByUser(
        [FromRoute] Guid userId,
        [FromRoute] Guid gameId
    )
    {
        try
        {
            var user = _userRepository.GetById(userId);

            var game = _gameRepository.GetById(gameId);

            _userRepository.AddGameByUser(user, game);

            _userRepository.Update(user);

            return Created();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPatch]
    [Route("reset-password")]
    public async Task<IActionResult> UpdatePasswordByUser(
        [FromBody] UserPasswordUpdateInput input
    )
    {
        try
        {
            var user = _userRepository.GetById(input.Id);

            var userNewPassword = await _userService.UpdatePasswordUserService(user, input.CurrentPassword, input.NewPassword);

            if (userNewPassword == null)
                return BadRequest();

            _userRepository.Update(userNewPassword);

            return Created();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(
        [FromBody] UserUpdateInput input
    )
    {
        try
        {
            var hashPassword = _userRepository.GetById(input.Id).Password;

            var user = new User()
            {
                Id = input.Id,
                Name = input.Name,
                Password = hashPassword,
                Email = input.Email,
                Administrator = input.Administrator,
                Library = input.Library
            };

            _userRepository.Update(user);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteUser(
        [FromRoute] Guid id
    )
    {
        try
        {
            _userRepository.Delete(id);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
