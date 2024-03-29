﻿namespace Otus_project_server;

public class ExceptionHandler
{
    private static IDictionary<Type, IDictionary<Type, Func<ICommand, BaseException, ICommand>>> store = 
        new Dictionary<Type, IDictionary<Type, Func<ICommand, BaseException, ICommand>>>();

    public static void RegisterHandler(Type commandType, Type exceptionType, Func<ICommand, BaseException, ICommand> handler)
    {
        if (!store.ContainsKey(commandType))
            store[commandType] = new Dictionary<Type, Func<ICommand, BaseException, ICommand>>();
        
        store[commandType][exceptionType] = handler;
    }
    
    public static ICommand Handle(BaseException exception, ICommand command) 
    {
        Type[] commandTypes = new Type [] {command.GetType(), typeof(ICommand)};
        Type[] exceptionTypes = new Type [] {exception.GetType(), typeof(BaseException)};
        
        foreach (var commandType in commandTypes)
        {
            foreach (var exceptionType in exceptionTypes)
                if (store.ContainsKey(commandType) && store[commandType].ContainsKey(exceptionType))
                    return store[commandType][exceptionType](command, exception);
        }
        
        throw new Exception($"Exception handler is not registered for " +
            $"command type {commandTypes.First().Name} and exception {exceptionTypes.First().Name}.");   
    }

    public static Func<ICommand, BaseException, ICommand> WriteLog(ILogger logger) => 
        (command, exception) => new WriteMessageToLogCommand(logger, "ERROR while executing command " + 
            $"{command.GetType().Name}: {exception.Message}");

    public static Func<ICommand, BaseException, ICommand> QueueWriteLog(ILogger logger, IQueue queue) => 
        (command, exception) => new AddCommandToQueue (
            queue, 
            new WriteMessageToLogCommand(logger, "ERROR while executing command " + 
                $"{command.GetType().Name}: {exception.Message}"));
    
    public static Func<ICommand, BaseException, ICommand> QueueCommand(IQueue queue) => 
        (command, exception) => new AddCommandToQueue (queue, command);

    public static Func<ICommand, BaseException, ICommand> QueueQueueCommand(IQueue queue) => 
        (command, exception) => new AddCommandToQueue (queue, new AddCommandToQueue (queue, command));

    public static Func<ICommand, BaseException, ICommand> RepeatCommandOrWriteLog(ILogger logger, IQueue queue) => 
        (command, exception) => 
            command is RepeatableCommand repeatable && repeatable.RemainingTryCount > 0
                ? new AddCommandToQueue(queue, repeatable)
                : new WriteMessageToLogCommand(logger, $"Repeatable command is out of try count or " +
                    $"the command is not repeatable. Last error: {exception.Message}");
}
