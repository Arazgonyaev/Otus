namespace Otus_16_endpoint;

public class ActionCommand : ICommand
{
    private readonly IObject gameObject;
    private readonly Action<IObject> action;

    public ActionCommand(IObject gameObject, Action<IObject> action)
    {
        this.gameObject = gameObject;
        this.action = action;
    }

    public void Execute()
    {
        action(gameObject);
    }
}