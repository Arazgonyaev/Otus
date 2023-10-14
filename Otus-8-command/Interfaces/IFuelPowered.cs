namespace Otus_8_command.Interfaces;

public interface IFuelPowered
{
    int FuelAmount { get; set; }
    int FuelConsumptionRate { get; }
}
