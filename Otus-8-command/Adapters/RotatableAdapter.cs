using System;
using Otus_8_command.Interfaces;

namespace Otus_8_command.Adapters;

public class RotatableAdapter : IRotatable
{
    private readonly IUObject _uObject;
    
    public RotatableAdapter(IUObject uObject)
    {
        _uObject = uObject;
    }

    public int Direction {
        get => GetPropertyValue("Direction");
        set => _uObject.SetProperty("Direction", value);
    }
    public int AngularVelocity => GetPropertyValue("AngularVelocity");
    
    public int DirectionsCount => GetPropertyValue("DirectionsCount");

    private int GetPropertyValue(string propertyName)
    {
        var value = _uObject.GetProperty(propertyName);
        
        if (value is int i)
            return i;
        
        throw new ArgumentException($"Incorrect property value type for {propertyName}");
    }
}
