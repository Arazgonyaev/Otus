using System;

namespace Otus_14_vertical_scaling;

public class SimpleCollisionChecker : ICollisionChecker
{
    private readonly int objectSize;
    private readonly ILogger logger;

    public SimpleCollisionChecker(int objectSize, ILogger logger)
    {
        this.objectSize = objectSize;
        this.logger = logger;
    }

    public void Check(IMovable objectA, IMovable objectB)
    {
        bool isCollision = true;
        
        for (int i = 0; i < objectA.Position.Length; i++)
        {
            isCollision &= Math.Abs(objectA.Position[i] - objectB.Position[i]) < objectSize;
        }

        if (isCollision)
            logger.Log($"Objects {objectA.Id} ({string.Join(", ", objectA.Position)}) " +
                $"and {objectB.Id} ({string.Join(", ", objectB.Position)}) are in a collision.");
    }
}
