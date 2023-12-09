using System.Collections.Generic;

namespace Otus_14_vertical_scaling;

public interface IField
{
    void AddObject(IMovable movable);
    int[] GetAreaId(int[] position);
    HashSet<IMovable> GetAreaById(int[] areaId);
}
