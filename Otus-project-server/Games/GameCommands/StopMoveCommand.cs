namespace Otus_project_server;

public class StopMoveCommand : ICommand
{
    private readonly IMovable movable;

    public StopMoveCommand(IMovable movable)
    {
        this.movable = movable;
    }

    public void Execute()
    {
        movable.Velocity = movable.Velocity.Select(x => 0).ToArray();
    }
}
