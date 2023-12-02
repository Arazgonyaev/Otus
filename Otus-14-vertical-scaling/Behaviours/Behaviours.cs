using System;

namespace Otus_14_vertical_scaling;

public static class Behaviours
{
    public static Action<BehaviourOptions> Run => (options) => 
    {
        while(!options.IsStop) 
        {
            var c = options.Queue.Take();
            try 
            {
                Console.WriteLine("Run behaviour: execute command");
                c.Execute();
            }
            catch (Exception e) 
            {
                Console.WriteLine($"Got Exception executing command: {e.Message}");
            }
        }
    };
    
    public static Action<BehaviourOptions> SoftStop(int? maxCommandsCount) => (options) => 
    {
        var processedCommands = 0;
        while(!options.IsStop // stop immediately if HardStopCommand received working in SoftStop behaviour
            && !options.Queue.IsEmpty() 
            && (maxCommandsCount is null || processedCommands < maxCommandsCount)) 
        {
            var c = options.Queue.Take();
            try 
            {
                Console.WriteLine("SoftStop behaviour: execute command");
                c.Execute();
                processedCommands++;
            }
            catch (Exception e) 
            {
                Console.WriteLine($"Got Exception executing command: {e.Message}");
            }
        }
    };
    
    public static Action<BehaviourOptions> MoveTo(IQueue queueMoveTo) => (options) => 
    {
        while(!options.Queue.IsEmpty()) 
        {
            Console.WriteLine("MoveTo behaviour: move command to other queue");
            queueMoveTo.Add(options.Queue.Take());
        }
    };
}
