namespace Otus_project_server;

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
