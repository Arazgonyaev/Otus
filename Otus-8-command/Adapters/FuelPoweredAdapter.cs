using System;
using Otus_8_command.Interfaces;

namespace Otus_8_command.Adapters;

public class FuelPoweredAdapter : IFuelPowered
{
    private readonly IUObject _uObject;

    public FuelPoweredAdapter(IUObject uObject)
    {
        _uObject = uObject;
    }

    public int FuelAmount
    {
        get => GetPropertyValue("FuelAmount");
        set => _uObject.SetProperty("FuelAmount", value);
    }

    public int FuelConsumptionRate => GetPropertyValue("FuelConsumptionRate");

    private int GetPropertyValue(string propertyName)
    {
        var value = _uObject.GetProperty(propertyName);

        if (value is int i)
            return i;

        throw new ArgumentException($"Incorrect property value type for {propertyName}");
    }
}
