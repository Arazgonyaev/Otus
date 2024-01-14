namespace Otus_16_endpoint;

public interface IGame
{
    string GameId {get;}

    void AddObject(IUObject gameObject);

    void AddCommand(ICommandFactory factory, string objectId, string jsonArgs);
}
