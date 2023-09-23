namespace Otus_5_stable_abstractions.Interfaces
{
    public interface IRotatable
    {
        int Direction { get; set; }
        int AngularVelocity { get; }
        int DirectionsCount { get; }
    }
}
