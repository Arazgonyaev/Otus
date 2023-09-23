using System;

namespace Otus_5_stable_abstractions.TestObjects;

public class NonMovableShip : UShip
{
    public NonMovableShip((int, int) position, (int, int) velocity, int direction, int angularVelocity, int directionsCount = 360) 
        : base(position, velocity, direction, angularVelocity, directionsCount)
    {
    }
    
    public override void SetProperty(string name, object value)
    {
        switch (name)
        {
            case "Position":
                throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}");
            case "Direction":
                Direction = GetCorrectValue<int>(value);
                break;
            default:
                throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}");
        }
    }
}