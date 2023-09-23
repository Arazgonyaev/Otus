using System;

namespace Otus_5_stable_abstractions.TestObjects;

public class NonVelocitibleShip : UShip
{
    public NonVelocitibleShip((int, int) position, (int, int) velocity, int direction, int angularVelocity, int directionsCount = 360) 
        : base(position, velocity, direction, angularVelocity, directionsCount)
    {
    }
    
    public override object GetProperty(string name)
    {
        return name switch
        {
            "Position" => Position,
            "Velocity" => throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}"),
            "Direction" => Direction,
            "AngularVelocity" => AngularVelocity,
            "DirectionsCount" => DirectionsCount,
            _ => throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}")
        };
    }
}