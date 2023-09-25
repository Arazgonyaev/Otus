using System;

namespace Otus_5_stable_abstractions.TestObjects;

public class NonRotatableShip : UShip
{
    public NonRotatableShip(int[] position, int[] velocity, int direction, int angularVelocity, int directionsCount = 360) 
        : base(position, velocity, direction, angularVelocity, directionsCount)
    {
    }
    
    public override void SetProperty(string name, object value)
    {
        switch (name)
        {
            case "Position":
                Position = GetCorrectValue<int[]>(value);
                break;
            case "Direction":
                throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}");
            default:
                throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}");
        }
    }
}