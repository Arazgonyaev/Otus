using System;
using Otus_8_command.Interfaces;

namespace Otus_8_command.Adapters;

public class MovableAdapter : IMovable
{
    private readonly IUObject _uObject;
    
    public MovableAdapter(IUObject uObject)
    {
        _uObject = uObject;
    }

    public int[] Position 
    {
        get => GetPropertyValue<int[]>("Position");
        set => _uObject.SetProperty("Position", value);
    }

    public int Velocity => GetPropertyValue<int>("Velocity");
    
    public int[] VelocityProjections 
    {
        get => GetPropertyValue<int[]>("VelocityProjections");
        set => _uObject.SetProperty("VelocityProjections", value);
    }

    protected T GetPropertyValue<T>(string propertyName)
    {
        var value = _uObject.GetProperty(propertyName);
        
        if (value is T correctValue)
            return correctValue;
        
        throw new ArgumentException($"Incorrect property value type for {propertyName}");
    }
}
