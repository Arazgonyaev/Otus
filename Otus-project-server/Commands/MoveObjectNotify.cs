namespace Otus_project_server;

public class MoveObjectNotify : ICommand
{
    private readonly IMovable movable;
    private readonly INotifier notifier;

    public MoveObjectNotify(IMovable movable, INotifier notifier)
    {
        this.movable = movable;
        this.notifier = notifier;
    }

    public void Execute()
    {
        notifier.ObjectMoved(movable.Id, movable.Position);
    }
}