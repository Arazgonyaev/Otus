namespace Otus_14_vertical_scaling;

public class MoveCommand : ICommand
{
    private readonly IMovable movable;

    public MoveCommand(IMovable movable)
    {
        this.movable = movable;
    }

    public void Execute()
    {
        movable.PreviousPosition = movable.Position;
        movable.Position = movable.Position.Plus(movable.Velocity);
    }
}
