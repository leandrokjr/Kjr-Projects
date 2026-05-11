using Asp.Versioning;
using CloudGames.Domain.Entities;
using CloudGames.Domain.Inputs;
using CloudGames.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CloudGames.HttpApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class GameController : ControllerBase
{
    private readonly IGameRepository _gameRepository;

    public GameController(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame(
        [FromBody] GameCreateInput input
    )
    {
        try
        {
            var game = new Game()
            {
               Title = input.Title,
               Price = input.Price,
               CurrentPrice = input.Price
            };

            _gameRepository.Create(game);

            return Created();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetGameById(
        [FromRoute] Guid id
    )
    {
        try
        {
            var game = _gameRepository.GetById(id);

            return Ok(game);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGames()
    {
        try
        {
            var games = _gameRepository.GetAll().ToList();

            return Ok(games);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPatch]
    [Route("{id}/promotion/{percentage}")]
    public async Task<IActionResult> AddGamePromotion(
        [FromRoute] Guid id,
        [FromRoute] int percentage
    )
    {
        try
        {
            var gameById = _gameRepository.GetById(id);

            var pricePromotion = gameById.Price - (gameById.Price * percentage / 100);

            var game = new Game()
            {
                Id = gameById.Id,
                Title = gameById.Title,
                Price = gameById.Price,
                CurrentPrice = pricePromotion
            };

            _gameRepository.Update(game);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGame(
        [FromBody] GameUpdateInput input
    )
    {
        try
        {
            var game = new Game()
            {
                Id = input.Id,
                Title = input.Title,
                Price = input.Price,
                CurrentPrice = input.CurrentPrice
            };

            _gameRepository.Update(game);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGame(
        [FromBody] Guid id
    )
    {
        try
        {
            _gameRepository.Delete(id);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
