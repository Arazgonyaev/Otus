namespace Otus_project_server;

public class RepeatableCommand : ICommand
{
    private readonly ICommand command;
    public int RemainingTryCount {get; private set; }

    public RepeatableCommand(ICommand command, int tryCount)
    {
        this.command = command ?? throw new ArgumentNullException(nameof(command));
        RemainingTryCount = tryCount > 0 ? tryCount : throw new ArgumentOutOfRangeException(nameof(tryCount));
    }

    public void Execute()
    {
        if (--RemainingTryCount < 0)
            throw new CommandException($"Repeatable command is out of try count");

        command.Execute();
    }
}
