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
        [FromBody] UserCreateInput input
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
        [FromBody] UserUpdateInput input
    )
    {
        try
        {
            var user = new User()
            {
                Id = input.Id,
                Name = input.Name,
                Email = input.Email,
                Password = input.Password,
                Administrator = input.Administrator
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
