using CloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudGames.HttpApi.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LibraryController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public LibraryController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpPatch]
    [Route("{userId}/game/{gameId}")]
    [AllowAnonymous]
    public async Task<IActionResult> AddGameByUser(
        [FromRoute] Guid userId,
        [FromRoute] Guid gameId
    )
    {
        var user = await _libraryService.AddGameByUser(userId, gameId);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
}

