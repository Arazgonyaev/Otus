namespace Otus_project_authorization;

public interface IGameService
{
    string CreateGame(IEnumerable<string> usernames);

    IEnumerable<string> GetGameUsers(string gameId);
}
