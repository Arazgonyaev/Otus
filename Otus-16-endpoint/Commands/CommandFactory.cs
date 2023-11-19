namespace Otus_16_endpoint;

public class CommandFactory : ICommandFactory
{
    public string OperationId {get;}
    private Func<IObject, string, ICommand> factory;

    public CommandFactory(string operationId, Func<IObject, string, ICommand> factory)
    {
        OperationId = operationId;
        this.factory = factory;
    }

    public ICommand Create(IObject gameObject, string jsonArgs) => factory(gameObject, jsonArgs);
}
