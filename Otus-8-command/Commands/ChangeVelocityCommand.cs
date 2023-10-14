using Otus_8_command.Interfaces;

namespace Otus_8_command;

public class ChangeVelocityCommand : ICommand
{
    readonly IMovable _movable;
    readonly IRotatable _rotatable;
    

    public ChangeVelocityCommand(IMovable movable, IRotatable rotatable)
    {
        _movable = movable;
        _rotatable = rotatable;
    }

    public void Execute()
    {
        _movable.VelocityProjections = _movable.Velocity.GetVelocityProjections(_rotatable.Direction, _rotatable.DirectionsCount);
    }
}
