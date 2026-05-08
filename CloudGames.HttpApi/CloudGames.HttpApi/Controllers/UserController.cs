using CloudGames.Domain.Entities;
using CloudGames.Domain.Inputs;
using CloudGames.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CloudGames.HttpApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromBody] UserInput input
    )
    {
        try
        {
            var user = new User()
            {
                Name = input.Name,
                Email = input.Email,
                Password = input.Password,
                Administrator = input.Administrator
            };

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
            _userRepository.GetById(id);

            return Ok();
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
            _userRepository.GetAll();

            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(
        [FromBody] UserInput input
    )
    {
        try
        {
            var user = new User()
            {
                Name = input.Name,
                Email = input.Email,
                Password = input.Password,
                Administrator = input.Administrator
            };

            _userRepository.Create(user);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(
        [FromBody] UserInput input
    )
    {
        try
        {
            var user = new User()
            {
                Name = input.Name,
                Email = input.Email,
                Password = input.Password,
                Administrator = input.Administrator
            };

            _userRepository.Create(user);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
