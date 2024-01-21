namespace Otus_project_server;

public class MacroCommand : ICommand
{
    readonly ICommand[] _commands;

    public MacroCommand(params ICommand[] commands)
    {
        _commands = commands;
    }

    public void Execute()
    {
        foreach (var command in _commands)
        {
            command.Execute();
        }
    }
}