namespace Otus_project_server;

public interface ICommandFactory
{
    string OperationId {get;}
    ICommand Create(IUObject gameObject, string jsonArgs);
}
