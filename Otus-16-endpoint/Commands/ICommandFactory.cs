namespace Otus_16_endpoint;

public interface ICommandFactory
{
    string OperationId {get;}
    ICommand Create(IUObject gameObject, string jsonArgs);
}
