using System.Collections.Generic;
using Otus_8_command.Interfaces;
using ArgumentException = System.ArgumentException;

namespace Otus_8_command;

public class UShip : IUObject
{
    protected Dictionary<string, object> Properties;
  
    public UShip(Dictionary<string, object> properties)
    {
        Properties = properties;

        if (!Properties.ContainsKey("DirectionsCount"))
            Properties["DirectionsCount"] = 360;

        // В конструктор корабля вектор мгновенной скорости должен быть передан в виде модуля и направления.
        // Вычисляем из этих значений проекции вектора на координатные оси, 
        // чтобы снизить трудоемкость команды примолинейного движения.
        // Внимание: при изменении направления корабля проекции скорости должны быть пересчитаны 
        // (используйте команду RotateWithChangeVelocityCommand).
        CalculateVelocity();
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

    protected void CalculateVelocity()
    {
        if (!Properties.ContainsKey("Direction"))
            Properties["Direction"] = 0;

        if (!Properties.ContainsKey("Velocity"))
            Properties["Velocity"] = 0;

        Properties["VelocityProjections"] = ((int)Properties["Velocity"])
            .GetVelocityProjections((int)Properties["Direction"], (int)Properties["DirectionsCount"]);
    }
}