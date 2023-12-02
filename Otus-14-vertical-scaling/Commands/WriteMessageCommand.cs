using System;

namespace Otus_14_vertical_scaling;

public class WriteMessageCommand : ICommand
{
    private readonly Action<string> write;
    private readonly string message;

    public WriteMessageCommand(Action<string> write, string message)
    {
        this.write = write;
        this.message = message;
    }

    public void Execute()
    {
        write(message);
    }
}
