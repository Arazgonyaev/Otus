namespace Otus_14_vertical_scaling;

public class StartCommand : ICommand
{
    private readonly MyThread myThread;

    public StartCommand(MyThread myThread)
    {
        this.myThread = myThread;
    }

    public void Execute()
    {
        myThread.Start();
    }
}
