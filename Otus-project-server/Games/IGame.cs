namespace Otus_project_server;

public interface IGame
{
    string GameId {get;}

    void AddObject(IUObject gameObject);

    void AddCommand(ICommandFactory factory, string objectId, string jsonArgs);
}
