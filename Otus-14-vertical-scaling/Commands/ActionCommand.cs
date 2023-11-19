using System;

namespace Otus_14_vertical_scaling;

public class ActionCommand : ICommand
{
    private readonly Action action;

    public ActionCommand(Action action)
    {
        this.action = action;
    }

    public void Execute()
    {
        action();
    }
}