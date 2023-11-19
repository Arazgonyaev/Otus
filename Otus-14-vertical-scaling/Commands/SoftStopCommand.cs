namespace Otus_14_vertical_scaling;

public class SoftStopCommand : ICommand
{
    private readonly MyThread myThread;
    private readonly int? maxCommandsCount;

    public SoftStopCommand(MyThread myThread, int? maxCommandsCount = null)
    {
        this.myThread = myThread;
        this.maxCommandsCount = maxCommandsCount;
    }

    public void Execute()
    {
        myThread.SetBehaviour(Behaviours.SoftStop(maxCommandsCount));
    }
}