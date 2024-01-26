namespace Otus_project_server;

public class Game2DField : IField
{
    private readonly int maxX;
    private readonly int maxY;
    private readonly int areaSize;
    private readonly int areaDelta;
    private readonly HashSet<IMovable>[][] lay;
    
    public Game2DField(int maxX, int maxY, int areaSize = 100, int areaDelta = 0)
    {
        this.maxX = maxX;
        this.maxY = maxY;
        this.areaSize = areaSize;
        this.areaDelta = areaDelta;
        lay = InitLay(areaDelta);
    }

    private HashSet<IMovable>[][] InitLay(int delta)
    {
        int countX = (int)Math.Ceiling((double)(maxX + delta)/areaSize);
        int countY = (int)Math.Ceiling((double)(maxY + delta)/areaSize);

        var lay = new HashSet<IMovable>[countX][];
        for (int i = 0; i < countX; i++)
        {
            lay[i] = new HashSet<IMovable>[countY];
            for (int j = 0; j < countY; j++)
            {
                lay[i][j] = new HashSet<IMovable>();
            }
        }

        return lay;
    }

    public void AddObject(IMovable movable)
    {
        GetAreaById(GetAreaId(movable.Position)).Add(movable);
    }

    public void DelObject(IMovable movable)
    {
        var areaId = GetAreaId(movable.Position);
        var r = lay[areaId[0]][areaId[1]].RemoveWhere((m) => m.Id == movable.Id);
    }
  
    public int[] GetAreaId(int[] position)
    {
        var idx = (position[0] + areaDelta) / areaSize;
        var idy = (position[1] + areaDelta) / areaSize;
        
        return new []{ Math.Min(idx, lay.Length-1) , Math.Min(idy, lay[0].Length-1) };
    }

    public HashSet<IMovable> GetAreaById(int[] areaId)
    {
        return lay[areaId[0]][areaId[1]];
    }
}
