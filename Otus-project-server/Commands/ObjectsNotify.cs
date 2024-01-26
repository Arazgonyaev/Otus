namespace Otus_project_server;

public class ObjectsNotify : ICommand
{
    private readonly IMovable[] movables;
    private readonly INotifier notifier;

    public ObjectsNotify(IMovable[] movables, INotifier notifier)
    {
        this.movables = movables;
        this.notifier = notifier;
    }

    public void Execute()
    {
        var objects = movables.Select(m => (m.Id, m.Position)).ToArray();
        notifier.ObjectsUpdated(objects);
    }
}
