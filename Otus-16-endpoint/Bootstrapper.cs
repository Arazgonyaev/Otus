namespace Otus_16_endpoint;

public static class Bootstrapper
{
    public static void RegisterCommandFactories(IServiceCollection services)
    {
        /* Changes object state. Sample:
        curl -X 'POST' \
        'http://localhost:5000/message/Game1/Object2/ChangeState' \
        -H 'accept: *\/*' \
        -H 'Content-Type: application/json' \
        -d '"{\"NewState\": \"Active\"}"'
        */
        services.AddSingleton<ICommandFactory>(new CommandFactory("ChangeState", ChangeState));

        /* Writes object state based on template. Sample:
        curl -X 'POST' \
        'http://localhost:5000/message/Game1/Object2/PrintState' \
        -H 'accept: *\/*' \
        -H 'Content-Type: application/json' \
        -d '"{\"Tmpl\": \"Current state: {0}\"}"'
        Output: Current state: Active 
        */
        services.AddSingleton<ICommandFactory>(new CommandFactory("PrintState", PrintState));
    }

    private static Func<IObject, string, ICommand> ChangeState => (obj, args) => 
        new ActionCommand(obj, (_) => obj.ObjectState = args.GetArg("NewState"));
    
    private static Func<IObject, string, ICommand> PrintState => (obj, args) => 
        new ActionCommand(obj, (o)=>{Console.WriteLine(string.Format(args.GetArg("Tmpl"), o.ObjectState));});
}
