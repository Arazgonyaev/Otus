using Otus_8_command.Interfaces;

namespace Otus_8_command.Commands;

public class BurnFuelCommand : ICommand
{
    readonly IFuelPowered _fuelPowered;

    public BurnFuelCommand(IFuelPowered fuelPowered)
    {
        _fuelPowered = fuelPowered;
    }

    public void Execute()
    {
        _fuelPowered.FuelAmount -= _fuelPowered.FuelConsumptionRate;
    }
}
