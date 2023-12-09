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
                c.Execute();
            }
            catch (BaseException e) 
            {
                ExceptionHandler.Handle(e, c).Execute();
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
                c.Execute();
                processedCommands++;
            }
            catch (BaseException e) 
            {
                ExceptionHandler.Handle(e, c).Execute();
            }
        }
    };
    
    public static Action<BehaviourOptions> MoveTo(IQueue queueMoveTo) => (options) => 
    {
        while(!options.Queue.IsEmpty()) 
        {
            queueMoveTo.Add(options.Queue.Take());
        }
    };
}
