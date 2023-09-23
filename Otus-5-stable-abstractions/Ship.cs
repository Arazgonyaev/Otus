using Otus_5_stable_abstractions.Interfaces;

namespace Otus_5_stable_abstractions;

public class Ship : IMovable, IRotatable
{
    public Ship((int, int) position, (int, int) velocity, int direction, int angularVelocity, int directionsCount = 360)
    {
        Position = position;
        Velocity = velocity;
        AngularVelocity = angularVelocity;
        Direction = direction;
        DirectionsCount = directionsCount;
    }

    public (int, int) Position { get; set; }
    public (int, int) Velocity { get; }
    public int Direction { get; set; }
    public int AngularVelocity { get; }
    public int DirectionsCount { get; }
}