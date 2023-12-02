namespace Otus_14_vertical_scaling;

public class RunCommand : ICommand
{
    private readonly MyThread myThread;

    public RunCommand(MyThread myThread)
    {
        this.myThread = myThread;
    }

    public void Execute()
    {
        myThread.SetBehaviour(Behaviours.Run);
    }
}
