namespace Otus_project_server;

public class AddCommandToQueue : ICommand
{
    private readonly IQueue queue;
    private readonly ICommand command;

    public AddCommandToQueue(IQueue queue, ICommand command)
    {
        this.queue = queue ?? throw new ArgumentNullException(nameof(queue));
        this.command = command ?? throw new ArgumentNullException(nameof(command));
    }

    public void Execute()
    {
        queue.Add(command);
    }
}
