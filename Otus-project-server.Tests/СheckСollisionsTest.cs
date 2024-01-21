using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Otus_project_server.Tests;

[TestClass]
public class СheckСollisionsTest
{
    [TestMethod]
    public void NoCollision_SmallObjects()
    {
        // Два точечных объекта находятся рядом, столкновения нет

        // Arrange
        var ship1 = new MovableAdapter(new UShip(
            new Dictionary<string, object>
            {
                {"Id", "Ship1"},
                {"Position", new[]{12, 5}},
                {"Velocity", new[]{-7, 3}}, // will be (5, 8) after MoveCommand
                {"PreviousPosition", new[]{0, 0}},
            }));
        
        var ship2 = new MovableAdapter(new UShip(
            new Dictionary<string, object>
            {
                {"Id", "Ship2"},
                {"Position", new[]{3, 7}},
                {"Velocity", new[]{2, 3}},
                {"PreviousPosition", new[]{0, 0}},
            }));
        
        var field = new Game2DField(100, 100, 10);
        field.AddObject(ship1);
        field.AddObject(ship2);
        var queue = new BlockingCollectionQueue();
        var myThread = new GameThread(queue, Behaviours.Processing);
        var log = new List<string>();
        var logger = new ListLogger(log);
        var checker = new ActionCollisionChecker(1, (m) => LogCollision(logger, m)); // small objects
        var resetEvent = new ManualResetEvent(false);
        
        queue.Add(new MoveCommand(ship1, new[]{100, 100}));
        queue.Add(new СheckСollisionsCommand(ship1, new []{ field }, checker, queue));
        queue.Add(new MacroCommand(new SoftStopCommand(myThread), new ActionCommand(null, (_) => resetEvent.Set())));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        resetEvent.WaitOne();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        var expectedLog = new List<string>(); // no collision
        CollectionAssert.AreEqual(expectedLog, log, "Incorrect processing log");
    }

    [TestMethod]
    public void Collision_TheSamePosition()
    {
        // Столкновение: два объекта находятся в одной точке

        // Arrange
        var ship1 = new MovableAdapter(new UShip(
            new Dictionary<string, object>
            {
                {"Id", "Ship1"},
                {"Position", new[]{12, 5}},
                {"Velocity", new[]{-7, 3}}, // will be (5, 8) after MoveCommand
                {"PreviousPosition", new[]{0, 0}},
            }));
        
        var ship2 = new MovableAdapter(new UShip(
            new Dictionary<string, object>
            {
                {"Id", "Ship2"},
                {"Position", new[]{5, 8}},
                {"Velocity", new[]{0, 0}},
                {"PreviousPosition", new[]{0, 0}},
            }));
        
        var field = new Game2DField(100, 100, 10);
        field.AddObject(ship1);
        field.AddObject(ship2);
        var queue = new BlockingCollectionQueue();
        var myThread = new GameThread(queue, Behaviours.Processing);
        var log = new List<string>();
        var logger = new ListLogger(log);
        var checker = new ActionCollisionChecker(5, (m) => LogCollision(logger, m));
        var resetEvent = new ManualResetEvent(false);
        
        queue.Add(new MoveCommand(ship1, new[]{100, 100}));
        queue.Add(new СheckСollisionsCommand(ship1, new []{ field }, checker, queue));
        queue.Add(new MacroCommand(new SoftStopCommand(myThread), new ActionCommand(null, (_) => resetEvent.Set())));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        resetEvent.WaitOne();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        var expectedLog = new []{
            "Objects Ship1 (5, 8) and Ship2 (5, 8) are in a collision.", 
        };
        CollectionAssert.AreEqual(expectedLog, log, "Incorrect processing log");
    }

    [TestMethod]
    public void Collision_DifferentPosition_SameArea()
    {
        // Столкновение: два объекта находятся рядом в одной окрестности, их размеры перекрываются

        // Arrange
        var ship1 = new MovableAdapter(new UShip(
            new Dictionary<string, object>
            {
                {"Id", "Ship1"},
                {"Position", new[]{10, 5}},
                {"Velocity", new[]{-7, 2}}, // will be (3, 7) after MoveCommand
                {"PreviousPosition", new[]{0, 0}},
            }));
        
        var ship2 = new MovableAdapter(new UShip(
            new Dictionary<string, object>
            {
                {"Id", "Ship2"},
                {"Position", new[]{5, 8}},
                {"Velocity", new[]{0, 0}},
                {"PreviousPosition", new[]{0, 0}},
            }));
        
        var field = new Game2DField(100, 100, 10);
        field.AddObject(ship1);
        field.AddObject(ship2);
        var queue = new BlockingCollectionQueue();
        var myThread = new GameThread(queue, Behaviours.Processing);
        var log = new List<string>();
        var logger = new ListLogger(log);
        var checker = new ActionCollisionChecker(5, (m) => LogCollision(logger, m));
        var resetEvent = new ManualResetEvent(false);
        
        queue.Add(new MoveCommand(ship1, new[]{100, 100}));
        queue.Add(new СheckСollisionsCommand(ship1, new []{ field }, checker, queue));
        queue.Add(new MacroCommand(new SoftStopCommand(myThread), new ActionCommand(null, (_) => resetEvent.Set())));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        resetEvent.WaitOne();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        var expectedLog = new []{
            "Objects Ship1 (3, 7) and Ship2 (5, 8) are in a collision.",
        };
        CollectionAssert.AreEqual(expectedLog, log, "Incorrect processing log");
    }

    [TestMethod]
    public void NoCollision_DifferentArea_OneAreaSystem()
    {
        // Два объекта находятся рядом в разных окрестности, их размеры перекрываются,
        // но столкновение на найдено, т.к. используется одна система окрестностей

        // Arrange
        var ship1 = new MovableAdapter(new UShip(
            new Dictionary<string, object>
            {
                {"Id", "Ship1"},
                {"Position", new[]{10, 5}},
                {"Velocity", new[]{1, 2}}, // will be (11, 7) after MoveCommand
                {"PreviousPosition", new[]{0, 0}},
            }));
        
        var ship2 = new MovableAdapter(new UShip(
            new Dictionary<string, object>
            {
                {"Id", "Ship2"},
                {"Position", new[]{9, 8}},
                {"Velocity", new[]{0, 0}},
                {"PreviousPosition", new[]{0, 0}},
            }));
        
        var field = new Game2DField(100, 100, 10);
        field.AddObject(ship1);
        field.AddObject(ship2);
        var queue = new BlockingCollectionQueue();
        var myThread = new GameThread(queue, Behaviours.Processing);
        var log = new List<string>();
        var logger = new ListLogger(log);
        var checker = new ActionCollisionChecker(5, (m) => LogCollision(logger, m));
        var resetEvent = new ManualResetEvent(false);
        
        queue.Add(new MoveCommand(ship1, new[]{100, 100}));
        queue.Add(new СheckСollisionsCommand(ship1, new []{ field }, checker, queue));
        queue.Add(new MacroCommand(new SoftStopCommand(myThread), new ActionCommand(null, (_) => resetEvent.Set())));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        resetEvent.WaitOne();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        var expectedLog = new List<string>(); // no collision
        CollectionAssert.AreEqual(expectedLog, log, "Incorrect processing log");
    }

    [TestMethod]
    public void Collision_DifferentArea_TwoAreaSystems()
    {
        // То же, что и в предыдущем тесте,
        // но столкновение найдено, т.к. используется две системы окрестностей

        // Arrange
        var ship1 = new MovableAdapter(new UShip(
            new Dictionary<string, object>
            {
                {"Id", "Ship1"},
                {"Position", new[]{10, 5}},
                {"Velocity", new[]{1, 2}}, // will be (11, 7) after MoveCommand
                {"PreviousPosition", new[]{0, 0}},
            }));
        
        var ship2 = new MovableAdapter(new UShip(
            new Dictionary<string, object>
            {
                {"Id", "Ship2"},
                {"Position", new[]{9, 8}},
                {"Velocity", new[]{0, 0}},
                {"PreviousPosition", new[]{0, 0}},
            }));
        
        var field1 = new Game2DField(100, 100, 10);
        field1.AddObject(ship1);
        field1.AddObject(ship2);
        var field2 = new Game2DField(100, 100, 10, areaDelta: 5); // вторая система окрестностей со смещением
        field2.AddObject(ship1);
        field2.AddObject(ship2);
        var queue = new BlockingCollectionQueue();
        var myThread = new GameThread(queue, Behaviours.Processing);
        var log = new List<string>();
        var logger = new ListLogger(log);
        var checker = new ActionCollisionChecker(5, (m) => LogCollision(logger, m));
        var resetEvent = new ManualResetEvent(false);
        
        queue.Add(new MoveCommand(ship1, new[]{100, 100}));
        queue.Add(new СheckСollisionsCommand(ship1, new []{ field1, field2 }, checker, queue));
        queue.Add(new MacroCommand(new SoftStopCommand(myThread), new ActionCommand(null, (_) => resetEvent.Set())));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        resetEvent.WaitOne();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        var expectedLog = new []{
            "Objects Ship1 (11, 7) and Ship2 (9, 8) are in a collision.",
        };
        CollectionAssert.AreEqual(expectedLog, log, "Incorrect processing log");
    }

    private void LogCollision(ILogger logger, IMovable[] objects)
    {
        logger.Log($"Objects {objects[0].Id} ({string.Join(", ", objects[0].Position)}) " +
            $"and {objects[1].Id} ({string.Join(", ", objects[1].Position)}) are in a collision.");

    }
}