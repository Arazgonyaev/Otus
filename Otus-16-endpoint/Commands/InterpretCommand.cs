namespace Otus_16_endpoint;

public class InterpretCommand : ICommand
{
    private readonly IGame game;
    private readonly ICommandFactory factory;
    private readonly string objectId;
    private readonly string jsonArgs;

    public InterpretCommand(IGame game, ICommandFactory factory, string objectId, string jsonArgs)
    {
        this.game = game;
        this.factory = factory;
        this.objectId = objectId;
        this.jsonArgs = jsonArgs;
    }

    public void Execute()
    {
        game.AddCommand(factory, objectId, jsonArgs);
    }
}
