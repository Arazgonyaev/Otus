using Microsoft.AspNetCore.Authentication.JwtBearer;
using Otus_project_server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ServiceConfiguration.SwaggerOptions);
builder.Services.AddControllers();

var config = builder.Configuration;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(ServiceConfiguration.JwtOptions(config["Jwt:Issuer"], config["Jwt:Key"]));

// Register services
builder.Services.AddSingleton<IMessageRegistrator, MessageRegistrator>();

// Register commands
Bootstrapper.RegisterCommandFactories(builder.Services);

// Register games
GameFactory.CreateSome("http://127.0.0.1:5000/game").ToList().ForEach(g => builder.Services.AddSingleton<IGame>(g));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
