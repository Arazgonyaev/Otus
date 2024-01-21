namespace Otus_project_server;

public class ActionCollisionChecker : ICollisionChecker
{
    private readonly int objectSize;
    private readonly Action<IMovable[]> action;

    public ActionCollisionChecker(int objectSize, Action<IMovable[]> action)
    {
        this.objectSize = objectSize;
        this.action = action;
    }

    public void Check(IMovable objectA, IMovable objectB)
    {
        bool isCollision = true;
        
        for (int i = 0; i < objectA.Position.Length; i++)
        {
            isCollision &= Math.Abs(objectA.Position[i] - objectB.Position[i]) < objectSize;
        }

        if (isCollision)
            action(new[]{ objectA, objectB});
    }
}
