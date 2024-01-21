using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Otus_project_server.Tests;

[TestClass]
public class GameCommandTest
{
    [TestMethod]
    public void StartMoveCommand()
    {
        // Arrange
        var resetEvent = new ManualResetEvent(false);
        var game = new SimpleGame("GameId", null);
        var gameObject = new UShip(new Dictionary<string, object>
        {
            {"Id", "Ship1"},
            {"Position", new[]{12, 5}},
            {"Velocity", new[]{0, 0}},
            {"PreviousPosition", new[]{12, 5}},
        });
        game.AddObject(gameObject);
        var commandFactory = new CommandFactory("StartMove", (obj, args) => 
            new MacroCommand(
                new StartMoveCommand(new MovableAdapter(gameObject), args.GetArg("InitVelocity").Split(",").Select(int.Parse).ToArray()),
                new ActionCommand(obj, (_) => resetEvent.Set())
        ));
        var registrator = new MessageRegistrator(new[]{game}, new[]{commandFactory});

        // Act
        registrator.RegisterMessage("GameId", "Ship1", "StartMove", "{\"InitVelocity\": \"2,2\"}");
        resetEvent.WaitOne(); // ensure message processed
        
        // Assert
        CollectionAssert.AreEqual(new[]{2, 2}, (int[])gameObject.GetProperty("Velocity"), "Object velocity must be changed");
    }

    [TestMethod]
    public void StopMoveCommand()
    {
        // Arrange
        var resetEvent = new ManualResetEvent(false);
        var game = new SimpleGame("GameId", null);
        var gameObject = new UShip(new Dictionary<string, object>
        {
            {"Id", "Ship1"},
            {"Position", new[]{12, 5}},
            {"Velocity", new[]{2, 2}},
            {"PreviousPosition", new[]{12, 5}},
        });
        game.AddObject(gameObject);
        var commandFactory = new CommandFactory("StopMove", (obj, args) => 
            new MacroCommand(
                new StopMoveCommand(new MovableAdapter(gameObject)),
                new ActionCommand(obj, (_) => resetEvent.Set())
        ));
        var registrator = new MessageRegistrator(new[]{game}, new[]{commandFactory});

        // Act
        registrator.RegisterMessage("GameId", "Ship1", "StopMove", null);
        resetEvent.WaitOne(); // ensure message processed
        
        // Assert
        CollectionAssert.AreEqual(new[]{0, 0}, (int[])gameObject.GetProperty("Velocity"), "Object velocity must be 0");
    }

    [TestMethod]
    public void ShotCommand()
    {
        // Arrange
        var resetEvent = new ManualResetEvent(false);
        var game = new SimpleGame("GameId", null);
        var gameObject = new UShip(new Dictionary<string, object>
        {
            {"Id", "Ship1"},
            {"ShellCnt", 5},
        });
        game.AddObject(gameObject);
        var commandFactory = new CommandFactory("Shot", (obj, args) => 
            new MacroCommand(
                new ShotCommand(new ShotableAdapter(gameObject)),
                new ActionCommand(obj, (_) => resetEvent.Set())
        ));
        var registrator = new MessageRegistrator(new[]{game}, new[]{commandFactory});

        // Act
        registrator.RegisterMessage("GameId", "Ship1", "Shot", null);
        resetEvent.WaitOne(); // ensure message processed
        
        // Assert
        Assert.AreEqual(4, (int)gameObject.GetProperty("ShellCnt"), "Object shell count must be changed");
    }

    [TestMethod]
    public void ShotCommandNoShells()
    {
        // Arrange
        var resetEvent = new ManualResetEvent(false);
        var game = new SimpleGame("GameId", null);
        var gameObject = new UShip(new Dictionary<string, object>
        {
            {"Id", "Ship1"},
            {"ShellCnt", 0},
        });
        game.AddObject(gameObject);
        var commandFactory = new CommandFactory("Shot", (obj, args) => 
            new MacroCommand(
                new ShotCommand(new ShotableAdapter(gameObject)),
                new ActionCommand(obj, (_) => resetEvent.Set())
        ));
        var registrator = new MessageRegistrator(new[]{game}, new[]{commandFactory});

        // Act
        registrator.RegisterMessage("GameId", "Ship1", "Shot", null);
        resetEvent.WaitOne(); // ensure message processed
        
        // Assert
        Assert.AreEqual(0, (int)gameObject.GetProperty("ShellCnt"), "Object shell count must not be changed");
    }
}