using System;
using System.Collections.Generic;

namespace Otus_5_stable_abstractions.TestObjects;

public class NonMovableShip : UShip
{
    public NonMovableShip(Dictionary<string, object> properties) : base(properties)
    {
    }
    
    public override void SetProperty(string name, object value)
    {
        CheckPropertyName(name);

        if (name == "Position")
            throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}");

        Properties[name] = value;
    }
}