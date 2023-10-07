using Otus_8_command.Interfaces;

namespace Otus_8_command.Commands;

public class MacroCommand : ICommand
{
    readonly ICommand[] _commands;

    public MacroCommand(ICommand[] commands)
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
