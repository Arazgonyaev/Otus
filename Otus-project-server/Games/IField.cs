namespace Otus_project_server;

public interface IField
{
    void AddObject(IMovable movable);
    int[] GetAreaId(int[] position);
    HashSet<IMovable> GetAreaById(int[] areaId);
    void DelObject(IMovable movable);
}
