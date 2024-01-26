namespace Otus_project_server;

public class StartCommand : ICommand
{
    private readonly GameThread myThread;

    public StartCommand(GameThread myThread)
    {
        this.myThread = myThread;
    }

    public void Execute()
    {
        myThread.Start();
    }
}
