namespace Otus_project_server;

public static class GameFactory
{
    private static int objectCounter;
    public static IEnumerable<IGame> CreateSome(string clientUrl)
    {
        return Enumerable.Range(1, 1).Select(n => new SimpleGame($"Game{n}", clientUrl).CreateSomeObjects());
    }

    public static IGame CreateSomeObjects(this IGame game, int count = 5)
    {
        var rnd = new Random();
        
        Enumerable.Range(1, count).ToList().ForEach(n => game.AddObject(new UShip(new Dictionary<string, object>
            {
                {"Id", $"Object{objectCounter++}"},
                {"State", "InitState"},
                {"Position", new[]{rnd.Next(800), rnd.Next(600)}},
                {"Velocity", new[]{rnd.Next(-30, 30), rnd.Next(-30, 30)}},
                {"PreviousPosition", new[]{rnd.Next(0, 0), rnd.Next(0, 0)}},
                {"ShellCnt", 10},
            })));
        
        return game;
    }
}
