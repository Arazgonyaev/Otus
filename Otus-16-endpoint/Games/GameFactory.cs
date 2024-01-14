namespace Otus_16_endpoint;

public static class GameFactory
{
    public static IEnumerable<IGame> CreateSome()
    {
        return Enumerable.Range(0, 5).Select(n => new SimpleGame($"Game{n}").CreateSomeObjects());
    }

    public static IGame CreateSomeObjects(this IGame game)
    {
        Enumerable.Range(0, 5).ToList().ForEach(n => game.AddObject(new UShip(new Dictionary<string, object>
            {
                {"Id", $"Object{n}"},
                {"State", "InitState"},
                {"Position", new[]{0, 0}},
                {"Velocity", new[]{0, 0}},
                {"PreviousPosition", new[]{0, 0}},
                {"ShellCnt", 10},
            })));
        
        return game;
    }
}
