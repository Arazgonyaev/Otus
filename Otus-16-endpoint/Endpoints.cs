using Microsoft.AspNetCore.Mvc;

namespace Otus_16_endpoint;

public static class Endpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/message/{gameId}/{objectId}/{operationId}", 
            (string gameId, string objectId, string operationId, [FromBody] string jsonstring, IMessageRegistrator registrator) => 
                {
                    try
                    {
                        registrator.RegisterMessage(gameId, objectId, operationId, jsonstring);
                        return Results.Ok();
                    }
                    catch (InvalidOperationException e)
                    {
                        return Results.BadRequest(e.Message);
                    }
                }
        )
        .WithName("RegisterMessage")
        .WithOpenApi();
    }
}
