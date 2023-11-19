namespace Otus_16_endpoint;

public interface ICommandFactory
{
    string OperationId {get;}
    ICommand Create(IObject gameObject, string jsonArgs);
}
