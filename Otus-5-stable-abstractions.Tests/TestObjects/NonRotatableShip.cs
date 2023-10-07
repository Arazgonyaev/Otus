using System;
using System.Collections.Generic;

namespace Otus_5_stable_abstractions.TestObjects;

public class NonRotatableShip : UShip
{
    public NonRotatableShip(Dictionary<string, object> properties) : base(properties)
    {
    }
    
    public override void SetProperty(string name, object value)
    {
        CheckPropertyName(name);

        if (name == "Direction")
            throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}");

        Properties[name] = value;
    }
}