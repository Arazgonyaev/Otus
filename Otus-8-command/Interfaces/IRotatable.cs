namespace Otus_8_command.Interfaces;

public interface IRotatable
{
    int Direction { get; set; }
    int AngularVelocity { get; }
    int DirectionsCount { get; }
}
