using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Otus_project_server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageRegistrator registrator;

    public MessageController(IMessageRegistrator registrator)
    {
        this.registrator = registrator;
    }

    [Authorize]
    [HttpPost]
    [Route("/message/{objectId}/{operationId}")]
    public IActionResult RegisterMessage(string objectId, string operationId, [FromBody] string jsonstring)
    {
        var gameId = User.FindFirstValue("gameId");
        var allowedCommands = User.FindAll("operation").Select(c => c.Value).ToList();
        if (!allowedCommands.Contains(operationId))
            return Forbid(); 

        try
        {
            registrator.RegisterMessage(gameId, objectId, operationId, jsonstring);
            return Ok();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }
}
