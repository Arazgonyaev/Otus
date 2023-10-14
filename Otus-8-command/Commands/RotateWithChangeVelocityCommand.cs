using Otus_8_command.Interfaces;

namespace Otus_8_command;

public class RotateWithChangeVelocityCommand : ICommand
{
    readonly IMovable _movable;
    readonly IRotatable _rotatable;

    public RotateWithChangeVelocityCommand(IMovable movable, IRotatable rotatable)
    {
        _movable = movable;
        _rotatable = rotatable;
    }

    public void Execute()
    {
        _rotatable.Direction = (_rotatable.Direction + _rotatable.AngularVelocity) % _rotatable.DirectionsCount;
        
        if (_movable.Velocity > 0)
            new ChangeVelocityCommand(_movable, _rotatable).Execute();
    }
}
