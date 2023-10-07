namespace Otus_8_command.Interfaces;

public interface IMovable
{
    int[] Position { get; set; }
    int Velocity { get; }
    int[] VelocityProjections { get; set; }
}
