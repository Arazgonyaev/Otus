using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Otus_19_authorization;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly IUserService userService;
    private readonly IGameService gameService;
    private readonly IJwtService jwtService;

    public TokenController(IUserService userService, IGameService gameService, IJwtService jwtService)
    {
        this.userService = userService;
        this.gameService = gameService;
        this.jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("/generate-game-token/{gameId}")]
    public IActionResult GenerateGameToken(string gameId, [FromBody] UserCredentials userCredentials)
    {
        try
        {
            if (!userService.IsValidUser(userCredentials) || 
                !gameService.GetGameUsers(gameId).Contains(userCredentials.Username)) 
                return Unauthorized();
            
            var token = jwtService.GenerateToken(userCredentials.Username, gameId);
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }
}
