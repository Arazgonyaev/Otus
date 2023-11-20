namespace Otus_19_authorization;

public interface IGameService
{
    string CreateGame(IEnumerable<string> usernames);

    IEnumerable<string> GetGameUsers(string gameId);
}
