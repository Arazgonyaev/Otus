using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Otus_project_authorization;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService gameService;

    public GameController(IGameService gameService)
    {
        this.gameService = gameService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("/create-game")]
    public IActionResult CreateGame([FromBody] IEnumerable<string> userNames)
    {
        string gameId = gameService.CreateGame(userNames);

        return Ok(new { gameId });
    }
}
