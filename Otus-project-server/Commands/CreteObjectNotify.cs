namespace Otus_project_server;

public class CreteObjectNotify : ICommand
{
    private readonly IMovable movable;
    private readonly INotifier notifier;

    public CreteObjectNotify(IMovable movable, INotifier notifier)
    {
        this.movable = movable;
        this.notifier = notifier;
    }

    public void Execute()
    {
        notifier.ObjectCreated(movable.Id, movable.Position);
    }
}
