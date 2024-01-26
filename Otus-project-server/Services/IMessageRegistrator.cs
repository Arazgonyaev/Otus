namespace Otus_project_server;

public interface IMessageRegistrator
{
    void RegisterMessage(string gameId, string objectId, string operationId, string jsonArgs);
}
