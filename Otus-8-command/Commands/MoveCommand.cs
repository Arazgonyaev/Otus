using Otus_8_command.Interfaces;

namespace Otus_8_command.Commands;

public class MoveCommand : ICommand
{
    readonly IMovable _movable;

    public MoveCommand(IMovable movable)
    {
        _movable = movable;
    }

    public void Execute()
    {
        _movable.Position = _movable.Position.Plus(_movable.VelocityProjections);
    }
}
