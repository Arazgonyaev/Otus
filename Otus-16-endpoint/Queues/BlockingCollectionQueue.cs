using System.Collections.Concurrent;

namespace Otus_16_endpoint;

public class BlockingCollectionQueue : IQueue
{
    private BlockingCollection<ICommand> queue = new();

    public void Add(ICommand command) => queue.Add(command);
    
    public ICommand Take() => queue.Take();
    
    public bool IsEmpty() => queue.Count == 0;

    public int Count() => queue.Count;
}
