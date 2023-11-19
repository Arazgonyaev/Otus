namespace Otus_16_endpoint;

public class MessageRegistrator : IMessageRegistrator
{
    private readonly IEnumerable<IGame> games;
    private readonly IEnumerable<ICommandFactory> commandFactiries;

    public MessageRegistrator(IEnumerable<IGame> games, IEnumerable<ICommandFactory> commandFactiries)
    {
        this.games = games;
        this.commandFactiries = commandFactiries;
    }

    public void RegisterMessage(string gameId, string objectId, string operationId, string jsonArgs)
    {
        IGame game = games.FirstOrDefault(g => g.GameId == gameId);
        if (game is null) throw new InvalidOperationException($"Game {gameId} not found");

        ICommandFactory factory = commandFactiries.FirstOrDefault(f => f.OperationId == operationId);
        if (factory is null) throw new InvalidOperationException($"Operation {operationId} not found");

        ICommand interpretCommand = new InterpretCommand(game, factory, objectId, jsonArgs);
        interpretCommand.Execute();
    }
}
