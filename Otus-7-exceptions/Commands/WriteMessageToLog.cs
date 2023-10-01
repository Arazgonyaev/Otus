using System;

namespace Otus_7_exceptions;

public class WriteMessageToLog : ICommand
{
    private readonly ILogger logger;
    private readonly string message;

    public WriteMessageToLog(ILogger logger, string message)
    {
        this.logger = logger;
        this.message = message;
    }

    public void Execute()
    {
        logger.Log(message);
    }
}
