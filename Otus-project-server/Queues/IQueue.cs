namespace Otus_project_server;

public interface IQueue
{
    void Add(ICommand command);
    ICommand Take();
    bool IsEmpty();
    int Count();
}
