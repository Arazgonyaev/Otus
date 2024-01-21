namespace Otus_14_vertical_scaling;

public class WriteMessageToLogCommand : ICommand
{
    private readonly ILogger logger;
    private readonly string message;

    public WriteMessageToLogCommand(ILogger logger, string message)
    {
        this.logger = logger;
        this.message = message;
    }

    public void Execute()
    {
        logger.Log(message);
    }
}
