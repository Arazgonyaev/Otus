namespace Otus_project_server;

public class MoveCommand : ICommand
{
    private readonly IMovable movable;
    private readonly int[] fieldSize;

    public MoveCommand(IMovable movable, int[] fieldSize)
    {
        this.movable = movable;
        this.fieldSize = fieldSize;
    }

    public void Execute()
    {
        movable.PreviousPosition = movable.Position;
        movable.Position = movable.Position.Plus(movable.Velocity);

        for (int i = 0; i < movable.Position.Length; i++)
        {
            if (movable.Position[i] > fieldSize[i])
            {
                movable.Position[i] = fieldSize[i] - 2*(movable.Position[i] - fieldSize[i]);
                movable.Velocity[i] = - movable.Velocity[i];
            }
            else if (movable.Position[i] < 0)
            {
                movable.Position[i] = - movable.Position[i];
                movable.Velocity[i] = - movable.Velocity[i];
            }
        }
    }
}
