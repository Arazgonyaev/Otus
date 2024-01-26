namespace Otus_project_server
{
    public interface IMovable
    {
        string Id { get; }
        int[] Position { get; set; }
        int[] Velocity { get; set; }
        int[] PreviousPosition { get; set; }
    }
}
