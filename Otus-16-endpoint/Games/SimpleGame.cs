using System.Collections.Concurrent;

namespace Otus_16_endpoint;

public class SimpleGame : IGame
{
    public string GameId {get;}
    private readonly IQueue commandQueue;
    private readonly GameThread gameThread;
    private readonly ConcurrentDictionary<string, IObject> gameObjects = new();

    public SimpleGame(string gameId)
    {
        GameId = gameId;
        commandQueue = new BlockingCollectionQueue();
        gameThread = new GameThread(commandQueue, Behaviours.Processing);
        gameThread.Start();
    }

    public void AddObject(IObject gameObject)
    {
        if (!gameObjects.TryAdd(gameObject.ObjectId, gameObject))
            throw new InvalidOperationException($"Object {gameObject.ObjectId} already exists");
    }

    public void AddCommand(ICommandFactory factory, string objectId, string jsonArgs)
    {
        if (gameObjects.TryGetValue(objectId, out IObject gameObject))
        {
            ICommand command = factory.Create(gameObject, jsonArgs);
            commandQueue.Add(command);
        }
        else 
            throw new InvalidOperationException($"Object {objectId} not found");
        
    }
}
