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
        services.AddSingleton<ICommandFactory>(new CommandFactory("ChangeState", (obj, args) => 
            new ActionCommand(obj, (_) => obj.SetProperty("State", args.GetArg("NewState")))));

        /* Writes object properties to console. Sample:
        curl -X 'POST' \
        'http://localhost:5000/message/Game1/Object2/PrintObject' \
        -H 'accept: *\/*' \
        -H 'Content-Type: application/json' \
        -d '""'
        Output: Current state: Active 
        */
        services.AddSingleton<ICommandFactory>(new CommandFactory("PrintObject", (obj, args) => 
            new ActionCommand(obj, (o)=>{Console.WriteLine(o.Stringify());})));

        // and so on ...
        services.AddSingleton<ICommandFactory>(new CommandFactory("StartMove", (obj, args) => 
            new StartMoveCommand(new MovableAdapter(obj), args.GetArg("InitVelocity").Split(",").Select(int.Parse).ToArray())));
        
        services.AddSingleton<ICommandFactory>(new CommandFactory("StopMove", (obj, args) => new StopMoveCommand(new MovableAdapter(obj))));
        
        services.AddSingleton<ICommandFactory>(new CommandFactory("Shot", (obj, args) => new ShotCommand(new ShotableAdapter(obj))));
    }
}
