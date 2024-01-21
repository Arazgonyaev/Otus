namespace Otus_project_server;

public interface ICollisionChecker
{
    void Check(IMovable objectIdA, IMovable objectIdB);
}
