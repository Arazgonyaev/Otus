
using System.Collections.Concurrent;

namespace Otus_project_authorization;

public class GameService : IGameService
{
    private ConcurrentDictionary<string, string[]> gameUsers = new();
    private object lockObj = new object();
    
    public string CreateGame(IEnumerable<string> usernames)
    {
        lock(lockObj)
        {
            string gameId = $"Game{gameUsers.Count()+1}";
            gameUsers[gameId] = usernames.ToArray();

            return gameId;
        }
    }

    public IEnumerable<string> GetGameUsers(string gameId)
    {
        if (gameUsers.TryGetValue(gameId, out string[] usernames))
        {
            return usernames;
        }

        throw new InvalidOperationException($"Game {gameId} not found");
    }
}
