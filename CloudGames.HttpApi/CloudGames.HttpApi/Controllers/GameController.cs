using Asp.Versioning;
using CloudGames.Application.Inputs;
using CloudGames.Application.Interfaces;
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
        var game = await _gameService.CreateGame(input);

        return Ok(game);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetGameById(
        [FromRoute] Guid id
    )
    {
        var game = await _gameService.GetGameById(id);

        if (game == null)
            return NotFound();

        return Ok(game);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllGames()
    {
        var games = await _gameService.GetAllGames();

        if (games == null)
            return NotFound();

        return Ok(games);
    }

    [HttpPatch]
    [Route("{id}/promotion/{percentage}")]
    public async Task<IActionResult> AddGamePromotion(
        [FromRoute] Guid id,
        [FromRoute] int percentage
    )
    {
        var game = await _gameService.CreateGamePromotion(id, percentage);

        if (game == null)
            return BadRequest();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGame(
        [FromBody] GameUpdateInput input
    )
    {
        var game = await _gameService.UpdateGame(input);

        if (game == null)
            return BadRequest();

        return Ok(game);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGame(
        [FromBody] Guid id
    )
    {
        await _gameService.DeleteGame(id);

        return NoContent();
    }
}
