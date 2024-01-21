namespace Otus_project_server;

public class ShotCommand : ICommand
{
    private readonly IShotable shotable;

    public ShotCommand(IShotable shotable)
    {
        this.shotable = shotable;
    }

    public void Execute()
    {
        if (shotable.ShellCnt > 0) shotable.ShellCnt -= 1;
    }
}
