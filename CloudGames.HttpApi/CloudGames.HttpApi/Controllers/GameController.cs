using CloudGames.Domain.Entities;
using CloudGames.Domain.Inputs;
using CloudGames.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CloudGames.HttpApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameRepository _gameRepository;

    public GameController(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame(
        [FromBody] GameInput input
    )
    {
        try
        {
            var game = new Game()
            {
               Title = input.Title,
               Price = input.Price,
               PricePromotion = input.PricePromotion
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
            _gameRepository.GetById(id);

            return Ok();
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
            _gameRepository.GetAll();

            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGame(
        [FromBody] GameInput input
    )
    {
        try
        {
            var game = new Game()
            {
                Title = input.Title,
                Price = input.Price,
                PricePromotion = input.PricePromotion
            };

            _gameRepository.Create(game);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGame(
        [FromBody] GameInput input
    )
    {
        try
        {
            var game = new Game()
            {
                Title = input.Title,
                Price = input.Price,
                PricePromotion = input.PricePromotion
            };

            _gameRepository.Create(game);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
