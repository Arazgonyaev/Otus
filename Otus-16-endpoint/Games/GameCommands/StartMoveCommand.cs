namespace Otus_16_endpoint;

public class StartMoveCommand : ICommand
{
    private readonly IMovable movable;
    private readonly int[] initVelocity;

    public StartMoveCommand(IMovable movable, int[] initVelocity)
    {
        this.movable = movable;
        this.initVelocity = initVelocity;
    }

    public void Execute()
    {
        movable.Velocity = initVelocity;
    }
}
