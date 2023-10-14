using Otus_8_command.Interfaces;

namespace Otus_8_command.Commands;

public class RotateCommand : ICommand
{
    readonly IRotatable _rotatable;

    public RotateCommand(IRotatable rotatable)
    {
        _rotatable = rotatable;
    }

    public void Execute()
    {
        _rotatable.Direction = (_rotatable.Direction + _rotatable.AngularVelocity) % _rotatable.DirectionsCount;
    }
}
