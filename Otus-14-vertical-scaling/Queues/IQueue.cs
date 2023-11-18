namespace Otus_14_vertical_scaling;

public interface IQueue
{
    void Add(ICommand command);
    ICommand Take();
    bool IsEmpty();
    int Count();
}
