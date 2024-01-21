namespace Otus_project_server;

public class CommandFactory : ICommandFactory
{
    public string OperationId {get;}
    private Func<IUObject, string, ICommand> factory;

    public CommandFactory(string operationId, Func<IUObject, string, ICommand> factory)
    {
        OperationId = operationId;
        this.factory = factory;
    }

    public ICommand Create(IUObject gameObject, string jsonArgs) => factory(gameObject, jsonArgs);
}
