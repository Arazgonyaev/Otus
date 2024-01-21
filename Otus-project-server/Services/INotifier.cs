namespace Otus_project_server;

public interface INotifier
{
    void ObjectCreated(string objectId, int[] position);
    void ObjectMoved(string objectId, int[] position);
    void ObjectsUpdated((string objectId, int[] position)[] objects);
}
