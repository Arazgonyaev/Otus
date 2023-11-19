using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Otus_16_endpoint.Tests;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public void MessageRegistrator()
    {
        // Arrange
        var resetEvent = new ManualResetEvent(false);
        var game = new SimpleGame("GameId");
        var gameObject = new SimpleObject("ObjectId"){ObjectState = "State1"};
        game.AddObject(gameObject);
        var commandFactory = new CommandFactory("ChangeState", (obj, args) => 
            new MacroCommand(
                new ActionCommand(obj, (_) => obj.ObjectState = args.GetArg("NewState")),
                new ActionCommand(obj, (_) => resetEvent.Set())
        ));
        var registrator = new MessageRegistrator(new[]{game}, new[]{commandFactory});

        // Act
        registrator.RegisterMessage("GameId", "ObjectId", "ChangeState", "{\"NewState\": \"State2\"}");
        resetEvent.WaitOne(); // ensure message processed
        
        // Assert
        Assert.AreEqual("State2", gameObject.ObjectState, "State2", "Object state must be changed");
    }
}