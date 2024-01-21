namespace Otus_project_server;

public class ActionCommand : ICommand
{
    private readonly IUObject gameObject;
    private readonly Action<IUObject> action;

    public ActionCommand(IUObject gameObject, Action<IUObject> action)
    {
        this.gameObject = gameObject;
        this.action = action;
    }

    public void Execute()
    {
        action(gameObject);
    }
}