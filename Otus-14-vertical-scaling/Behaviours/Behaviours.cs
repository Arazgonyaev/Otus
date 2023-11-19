using System;

namespace Otus_14_vertical_scaling;

public static class Behaviours
{
    public static Action<BehaviourOptions> Processing => (options) => 
    {
        while(!options.IsStop) 
        {
            var c = options.Queue.Take();
            try 
            {
                Console.WriteLine("Processing behaviour: execute command");
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
        while(!options.Queue.IsEmpty() && (maxCommandsCount is null || processedCommands < maxCommandsCount)) 
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
}
