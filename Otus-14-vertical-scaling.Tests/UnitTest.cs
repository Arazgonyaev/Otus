using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Otus_14_vertical_scaling.Tests;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public void StartCommand()
    {
        // Arrange
        var queue = new BlockingCollectionQueue();
        var myThread = new MyThread(queue, Behaviours.Processing);
        var resetEvent = new ManualResetEvent(false);
        queue.Add(new WriteMessageCommand("Command 1"));
        queue.Add(new WriteMessageCommand("Command 2"));
        queue.Add(new ActionCommand(() => { // sync command
            resetEvent.Set();
            resetEvent.WaitOne();
            }));
        var startCommand = new StartCommand(myThread);
        Assert.IsFalse(queue.IsEmpty(), "Queue must not be empty before start command processing");
        Assert.IsFalse(myThread.Thread.IsAlive, "Thread must not be alive before start command processing");

        // Act
        startCommand.Execute();

        // Assert
        Assert.IsTrue(myThread.Thread.IsAlive, "Thread must be alive after start command processing");
        
        // Check commands are being processing
        resetEvent.WaitOne(); // wait for processing sync command
        Assert.IsTrue(queue.IsEmpty(), "Queue must be empty after sync command processing");
        
        // Stop processing
        resetEvent.Set();
        queue.Add(new HardStopCommand(myThread));
        myThread.Thread.Join(); // wait for myThread complete
        Assert.IsFalse(myThread.Thread.IsAlive, "Thread must not be alive after stop command processing");
    }

    [TestMethod]
    public void HardStopCommand()
    {
        // Arrange
        var queue = new BlockingCollectionQueue();
        var myThread = new MyThread(queue, Behaviours.Processing);
        queue.Add(new WriteMessageCommand("Command 1"));
        queue.Add(new WriteMessageCommand("Command 2"));
        queue.Add(new HardStopCommand(myThread));
        queue.Add(new WriteMessageCommand("Command 3"));
        queue.Add(new WriteMessageCommand("Command 4"));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        Assert.AreEqual(2, queue.Count(), "Commands following HardStopCommand must not be processed");
    }

    [TestMethod]
    public void SoftStopCommand()
    {
        // Arrange
        var queue = new BlockingCollectionQueue();
        var myThread = new MyThread(queue, Behaviours.Processing);
        var resetEvent = new ManualResetEvent(false);
        queue.Add(new WriteMessageCommand("Command 1"));
        queue.Add(new WriteMessageCommand("Command 2"));
        queue.Add(new MacroCommand(new SoftStopCommand(myThread), new ActionCommand(() => resetEvent.Set())));
        queue.Add(new WriteMessageCommand("Command 3"));
        queue.Add(new WriteMessageCommand("Command 4"));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        resetEvent.WaitOne();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        Assert.IsTrue(queue.IsEmpty(), "Commands following SoftStopCommand must be processed");
    }

    [TestMethod]
    public void SoftStopProcessingNCommand()
    {
        // Arrange
        var maxCommandsCount = 3;
        var queue = new BlockingCollectionQueue();
        var myThread = new MyThread(queue, Behaviours.Processing);
        var resetEvent = new ManualResetEvent(false);
        queue.Add(new WriteMessageCommand("Command 1"));
        queue.Add(new WriteMessageCommand("Command 2"));
        queue.Add(new MacroCommand(new SoftStopCommand(myThread, maxCommandsCount), new ActionCommand(() => resetEvent.Set())));
        queue.Add(new WriteMessageCommand("Command 3"));
        queue.Add(new WriteMessageCommand("Command 4"));
        queue.Add(new WriteMessageCommand("Command 5"));
        queue.Add(new WriteMessageCommand("Command 6"));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        resetEvent.WaitOne();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        Assert.AreEqual(1, queue.Count(), $"Only {maxCommandsCount} commands following SoftStopCommand must be processed");
    }
}