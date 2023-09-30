using System;
using System.Collections.Concurrent;

namespace Otus_7_exceptions;

public class AddCommandToQueue : ICommand
{
    private readonly BlockingCollection<ICommand> queue;
    private readonly ICommand command;

    public AddCommandToQueue(BlockingCollection<ICommand> queue, ICommand command)
    {
        this.queue = queue ?? throw new ArgumentNullException(nameof(queue));
        this.command = command ?? throw new ArgumentNullException(nameof(command));
    }

    public void Execute()
    {
        queue.Add(command);
    }
}
