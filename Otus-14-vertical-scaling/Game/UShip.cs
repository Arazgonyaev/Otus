using System.Collections.Generic;
using ArgumentException = System.ArgumentException;

namespace Otus_14_vertical_scaling;

public class UShip : IUObject
{
    protected Dictionary<string, object> Properties;

    
    public UShip(Dictionary<string, object> properties)
    {
        Properties = properties;
        if (!Properties.ContainsKey("DirectionsCount"))
            Properties["DirectionsCount"] = 360;
    }

    public virtual object GetProperty(string name)
    {
        CheckPropertyName(name);
        
        return Properties[name];
    }

    public virtual void SetProperty(string name, object value)
    {
        CheckPropertyName(name);

        Properties[name] = value;
    }

    protected void CheckPropertyName(string name)
    {
        if (!Properties.ContainsKey(name))
            throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}");
    }
}