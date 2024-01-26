namespace Otus_project_server;

public static class Behaviours
{
    public static Action<BehaviourOptions> Processing => (options) => 
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
    
}
