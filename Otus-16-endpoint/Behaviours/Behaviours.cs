namespace Otus_16_endpoint;

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
            catch (Exception e) 
            {
                Console.WriteLine($"Got Exception executing command: {e.Message}");
            }
        }
    };
}
