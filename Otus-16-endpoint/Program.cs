using Otus_16_endpoint;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddSingleton<IMessageRegistrator, MessageRegistrator>();

// Register commands

/* 1. Changes object state, sample:
curl -X 'POST' \
  'http://localhost:5000/message/Game1/Object2/ChangeState' \
  -H 'accept: *\/*' \
  -H 'Content-Type: application/json' \
  -d '"{\"NewState\": \"Active\"}"'
*/
builder.Services.AddSingleton<ICommandFactory>(new CommandFactory("ChangeState", (obj, args) => 
        new ActionCommand(obj, (_) => obj.ObjectState = args.GetArg("NewState"))));

/* 2. Writes object state based on template, sample:
curl -X 'POST' \
  'http://localhost:5000/message/Game1/Object2/PrintState' \
  -H 'accept: *\/*' \
  -H 'Content-Type: application/json' \
  -d '"{\"Tmpl\": \"Current state: {0}\"}"'
Output: Current state: Active 
*/
builder.Services.AddSingleton<ICommandFactory>(new CommandFactory("PrintState", (obj, args) => 
    new ActionCommand(obj, (o)=>{Console.WriteLine(string.Format(args.GetArg("Tmpl"), o.ObjectState));})));

// Register games
GameFactory.CreateSome().ToList().ForEach(g => builder.Services.AddSingleton<IGame>(g));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
Endpoints.Map(app);
app.Run();
