namespace Otus_14_vertical_scaling;

public class MoveToCommand : ICommand
{
    private readonly MyThread myThread;
    private readonly IQueue queueMoveTo;

    public MoveToCommand(MyThread myThread, IQueue queueMoveTo)
    {
        this.myThread = myThread;
        this.queueMoveTo = queueMoveTo;
    }

    public void Execute()
    {
        myThread.SetBehaviour(Behaviours.MoveTo(queueMoveTo));
    }
}
