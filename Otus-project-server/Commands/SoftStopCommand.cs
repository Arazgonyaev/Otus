namespace Otus_project_server;

public class SoftStopCommand : ICommand
{
    private readonly GameThread myThread;
    private readonly int? maxCommandsCount;

    public SoftStopCommand(GameThread myThread, int? maxCommandsCount = null)
    {
        this.myThread = myThread;
        this.maxCommandsCount = maxCommandsCount;
    }

    public void Execute()
    {
        myThread.SetBehaviour(Behaviours.SoftStop(maxCommandsCount));
    }
}