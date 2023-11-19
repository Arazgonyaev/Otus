namespace Otus_16_endpoint;

public interface IQueue
{
    void Add(ICommand command);
    ICommand Take();
    bool IsEmpty();
    int Count();
}
