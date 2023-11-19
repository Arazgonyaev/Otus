namespace Otus_16_endpoint;

public interface IMessageRegistrator
{
    void RegisterMessage(string gameId, string objectId, string operationId, string jsonArgs);
}
