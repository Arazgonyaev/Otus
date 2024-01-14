using System.Collections;

namespace Otus_16_endpoint;

public class UShip : IUObject
{
    protected Dictionary<string, object> Properties;

    
    public UShip(Dictionary<string, object> properties)
    {
        Properties = properties;
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

    public string Stringify()
    {
        return string.Join("\n", Properties.Select(p => $"{p.Key}:\t{(p.Value is IEnumerable<int> e ? "["+string.Join(", ", e)+"]" : p.Value)}"));
    }

    protected void CheckPropertyName(string name)
    {
        if (!Properties.ContainsKey(name))
            throw new ArgumentException($"Incorrect property name '{name}' for {GetType().Name}");
    }
}
