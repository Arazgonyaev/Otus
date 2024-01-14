namespace Otus_16_endpoint
{
    public interface IMovable
    {
        string Id { get; }
        int[] Position { get; set; }
        int[] Velocity { get; set; }
        int[] PreviousPosition { get; set; }
    }
}
