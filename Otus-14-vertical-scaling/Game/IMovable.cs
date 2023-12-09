namespace Otus_14_vertical_scaling
{
    public interface IMovable
    {
        string Id { get; }
        int[] Position { get; set; }
        int[] Velocity { get; }
        int[] PreviousPosition { get; set; }
    }
}
