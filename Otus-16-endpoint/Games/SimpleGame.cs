using System.Collections.Concurrent;

namespace Otus_16_endpoint;

public class SimpleGame : IGame
{
    public string GameId {get;}
    private readonly IQueue commandQueue;
    private readonly GameThread gameThread;
    private readonly ConcurrentDictionary<string, IUObject> gameObjects = new();

    public SimpleGame(string gameId)
    {
        GameId = gameId;
        commandQueue = new BlockingCollectionQueue();
        gameThread = new GameThread(commandQueue, Behaviours.Processing);
        gameThread.Start();
    }

    public void AddObject(IUObject gameObject)
    {
        if (!gameObjects.TryAdd((string)gameObject.GetProperty("Id"), gameObject))
            throw new InvalidOperationException($"Object {gameObject.GetProperty("Id")} already exists");
    }

    public void AddCommand(ICommandFactory factory, string objectId, string jsonArgs)
    {
        if (gameObjects.TryGetValue(objectId, out IUObject gameObject))
        {
            ICommand command = factory.Create(gameObject, jsonArgs);
            commandQueue.Add(command);
        }
        else 
            throw new InvalidOperationException($"Object {objectId} not found");
        
    }
}
