namespace Otus_16_endpoint;

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
