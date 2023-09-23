namespace Otus_5_stable_abstractions.Interfaces
{
    public interface IMovable
    {
        (int, int) Position { get; set; }
        (int, int) Velocity { get; }
    }
}
