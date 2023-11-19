namespace Otus_14_vertical_scaling;

public class HardStopCommand : ICommand
{
    private readonly MyThread myThread;

    public HardStopCommand(MyThread myThread)
    {
        this.myThread = myThread;
    }

    public void Execute()
    {
        myThread.Stop();
    }
}
