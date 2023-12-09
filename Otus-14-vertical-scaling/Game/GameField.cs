using System;
using System.Collections.Generic;

namespace Otus_14_vertical_scaling;

public class Game2DField : IField
{
    private readonly int maxX;
    private readonly int maxY;
    private readonly int areaSize;
    private readonly int areaDelta;
    private readonly HashSet<IMovable>[][] lay;
    
    public Game2DField(int maxX, int maxY, int areaSize = 10, int areaDelta = 0)
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
    
    public int[] GetAreaId(int[] position)
    {
        return new []{ (position[0] + areaDelta) / areaSize, (position[1] + areaDelta) / areaSize };
    }

    public HashSet<IMovable> GetAreaById(int[] areaId)
    {
        return lay[areaId[0]][areaId[1]];
    }
}
