using System;
using System.Collections.Concurrent;

namespace Otus_7_exceptions;

public class QueueProcessor
{
    public static void ProcessWhileNotEmpty(BlockingCollection<ICommand> queue)
    { 
        var count = 0;

        while(queue.Count != 0) 
        {
            var command = queue.Take();

            try 
            {
                command.Execute();
            }
            catch (BaseException exception) 
            {
                ExceptionHandler.Handle(exception, command).Execute();
            }
            
            count += 1;
        }

        Console.WriteLine($"Processing completed, processed command count: {count}");
    }
}
