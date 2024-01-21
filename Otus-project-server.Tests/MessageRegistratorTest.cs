using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Otus_project_server.Tests;

[TestClass]
public class MessageRegistratorTest
{
    [TestMethod]
    public void MessageRegistrator()
    {
        // Arrange
        var resetEvent = new ManualResetEvent(false);
        var game = new SimpleGame("GameId", null);
        var gameObject = new UShip(new Dictionary<string, object>
            {
                {"Id", "ObjectId"},
                {"State", "State1"},
            });
        game.AddObject(gameObject);
        var commandFactory = new CommandFactory("ChangeState", (obj, args) => 
            new MacroCommand(
                new ActionCommand(obj, (_) => obj.SetProperty("State", args.GetArg("NewState"))),
                new ActionCommand(obj, (_) => resetEvent.Set())
        ));
        var registrator = new MessageRegistrator(new[]{game}, new[]{commandFactory});

        // Act
        registrator.RegisterMessage("GameId", "ObjectId", "ChangeState", "{\"NewState\": \"State2\"}");
        resetEvent.WaitOne(); // ensure message processed
        
        // Assert
        Assert.AreEqual("State2", gameObject.GetProperty("State").ToString(), "Object state must be changed");
    }
}