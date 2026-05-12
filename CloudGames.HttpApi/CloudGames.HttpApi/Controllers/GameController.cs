using Asp.Versioning;
using CloudGames.Application.Interfaces;
using CloudGames.Domain.Inputs;
using Microsoft.AspNetCore.Mvc;

namespace CloudGames.HttpApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame(
        [FromBody] GameCreateInput input
    )
    {
        try
        {
            var game = await _gameService.CreateGame(input);

            return Ok(game);
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
            var game = await _gameService.GetGameById(id);

            if (game == null)
                return NotFound();

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
            var games = await _gameService.GetAllGames();

            if (games == null)
                return NotFound();

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
            var game = await _gameService.CreateGamePromotion(id, percentage);

            if (game == null)
                return NotFound();

            return Ok();
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
            var game = _gameService.UpdateGame(input);

            return Ok(game);
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
            await _gameService.DeleteGame(id);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
