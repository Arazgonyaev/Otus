using System.Collections.Concurrent;
using System.Timers;

namespace Otus_project_server;

public class SimpleGame : IGame
{
    public string GameId { get; }
    private readonly int[] fieldSize = new[]{800, 600};
    private readonly IQueue commandQueue;
    private readonly GameThread gameThread;
    private readonly ConcurrentDictionary<string, IUObject> gameObjects = new();
    private INotifier notifier;
    private IField[] fields;
    private ICollisionChecker collisionChecker;
    private System.Timers.Timer timer;

    public SimpleGame(string gameId, string clientUrl)
    {
        GameId = gameId;
        commandQueue = new BlockingCollectionQueue();
        gameThread = new GameThread(commandQueue, Behaviours.Processing);
        gameThread.Start();
        CreateTimer();
        CreateNotifier(clientUrl);
        CreateFields();
        CreateCollisionChecker();
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
    
    private void CreateTimer()
    {
        timer = new System.Timers.Timer(100);
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    private void CreateNotifier(string clientUrl)
    {
        if (!string.IsNullOrEmpty(clientUrl))
            notifier = new HtttpNotifier(clientUrl);
    }

    private void CreateFields()
    {
        var field1 = new Game2DField(fieldSize[0], fieldSize[1], areaSize: 100);                  // система окрестностей для проверки коллизий
        var field2 = new Game2DField(fieldSize[0], fieldSize[1], areaSize: 100, areaDelta: 50);   // вторая система окрестностей со смещением
        fields = new[]{field1, field2};
    }

    private void CreateCollisionChecker()
    {
        collisionChecker = new ActionCollisionChecker(30, DelObjects);
    }

    private void DelObjects(params IMovable[] movables)
    {
        foreach (var movable in movables)
        {
            foreach (var field in fields)
            {
                field.DelObject(movable);
            }
            var t = gameObjects.TryRemove(movable.Id, out _);
            
            if (t) GameFactory.CreateSomeObjects(this, 1);
        }
    }
  
    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        MoveObjects();
    }

    private void MoveObjects()
    {
        foreach (var obj in gameObjects.Values)
        {
            var movable = new MovableAdapter(obj);
            if (movable.Velocity.Any(v => v != 0))
                MoveObject(movable);
        }

        ObjectsNotify();  // Уведомляем клиента после перемещения всех объектов
    }

    private void MoveObject(IMovable movable)
    {
        var command = new MacroCommand(
            new MoveCommand(movable, fieldSize),
            //new MoveObjectNotify(movable, notifier), // Уведомлять клиента о каждом объекте оказалось накладно
            new СheckСollisionsCommand(movable, fields, collisionChecker, commandQueue)
        );

        commandQueue.Add(command);
    }

    private void ObjectsNotify()
    {
        if (notifier is null) return;
        
        var movables = gameObjects.Values.Select(o => new MovableAdapter(o)).ToArray();
        var command = new ObjectsNotify(movables, notifier);

        commandQueue.Add(command);
    }
}
