using System;

namespace Otus_14_vertical_scaling;

public class WriteMessageCommand : ICommand
{
    private readonly string message;

    public WriteMessageCommand(string message)
    {
        this.message = message;
    }

    public void Execute()
    {
        Console.WriteLine(message);
    }
}
