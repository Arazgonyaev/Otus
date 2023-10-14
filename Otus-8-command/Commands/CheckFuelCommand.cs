using Otus_8_command.Interfaces;

namespace Otus_8_command.Commands;

public class CheckFuelCommand : ICommand
{
    readonly IFuelPowered _fuelPowered;

    public CheckFuelCommand(IFuelPowered fuelPowered)
    {
        _fuelPowered = fuelPowered;
    }

    public void Execute()
    {
        if (_fuelPowered.FuelAmount < _fuelPowered.FuelConsumptionRate)
            throw new CommandException("Fuel amount is not enough.");
    }
}
