using Otus_5_stable_abstractions.Interfaces;
using ArgumentException = System.ArgumentException;

namespace Otus_5_stable_abstractions;

public class UShip : IUObject
{
    protected (int, int) Position;
    protected (int, int) Velocity;
    protected int Direction;
    protected readonly int AngularVelocity;
    protected readonly int DirectionsCount;
    
    public UShip((int, int) position, (int, int) velocity, int direction, int angularVelocity, int directionsCount = 360)
    {
        Position = position;
        Velocity = velocity;
        Direction = direction;
        AngularVelocity = angularVelocity;
        DirectionsCount = directionsCount;
    }

    public virtual object GetProperty(string name)
    {
        return name switch
        {
            "Position" => Position,
            "Velocity" => Velocity,
            "Direction" => Direction,
            "AngularVelocity" => AngularVelocity,
            "DirectionsCount" => DirectionsCount,
            _ => throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}")
        };
    }

    public virtual void SetProperty(string name, object value)
    {
        switch (name)
        {
            case "Position":
                Position = GetCorrectValue<(int, int)>(value);
                break;
            case "Direction":
                Direction = GetCorrectValue<int>(value);
                break;
            default:
                throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}");
        }
    }

    protected T GetCorrectValue<T>(object value)
    {
        if (value is T correctValue)
            return correctValue;
        
        throw new ArgumentException($"Incorrect property value type for {GetType().Name}");
    }
}