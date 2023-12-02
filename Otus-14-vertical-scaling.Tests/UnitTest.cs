using System.Collections.Generic;
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
        var myThread = new MyThread(queue, Behaviours.Run);
        var resetEvent = new ManualResetEvent(false);
        var log = new List<string>();
        queue.Add(new WriteMessageCommand(log.Add, "Command 1"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 2"));
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

        // Check processing log
        var expectedLog = new []{"Command 1", "Command 2"};
        CollectionAssert.AreEqual(expectedLog, log, "Incorrect processing log");
    }

    [TestMethod]
    public void HardStopCommand()
    {
        // Arrange
        var queue = new BlockingCollectionQueue();
        var myThread = new MyThread(queue, Behaviours.Run);
        var log = new List<string>();
        queue.Add(new WriteMessageCommand(log.Add, "Command 1"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 2"));
        queue.Add(new HardStopCommand(myThread));
        queue.Add(new WriteMessageCommand(log.Add, "Command 3"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 4"));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        Assert.AreEqual(2, queue.Count(), "Commands following HardStopCommand must not be processed");
        var expectedLog = new []{"Command 1", "Command 2"};
        CollectionAssert.AreEqual(expectedLog, log, "Incorrect processing log");
    }

    [TestMethod]
    public void SoftStopCommand()
    {
        // Arrange
        var queue = new BlockingCollectionQueue();
        var myThread = new MyThread(queue, Behaviours.Run);
        var resetEvent = new ManualResetEvent(false);
        var log = new List<string>();
        queue.Add(new WriteMessageCommand(log.Add, "Command 1"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 2"));
        queue.Add(new MacroCommand(new SoftStopCommand(myThread), new ActionCommand(() => resetEvent.Set())));
        queue.Add(new WriteMessageCommand(log.Add, "Command 3"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 4"));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        resetEvent.WaitOne();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        Assert.IsTrue(queue.IsEmpty(), "Commands following SoftStopCommand must be processed");
        var expectedLog = new []{"Command 1", "Command 2", "Command 3", "Command 4"};
        CollectionAssert.AreEqual(expectedLog, log, "Incorrect processing log");
    }

    [TestMethod]
    public void SoftStopProcessingNCommand()
    {
        // Arrange
        var maxCommandsCount = 3;
        var queue = new BlockingCollectionQueue();
        var myThread = new MyThread(queue, Behaviours.Run);
        var resetEvent = new ManualResetEvent(false);
        var log = new List<string>();
        queue.Add(new WriteMessageCommand(log.Add, "Command 1"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 2"));
        queue.Add(new MacroCommand(new SoftStopCommand(myThread, maxCommandsCount), new ActionCommand(() => resetEvent.Set())));
        queue.Add(new WriteMessageCommand(log.Add, "Command 3"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 4"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 5"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 6"));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        resetEvent.WaitOne();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        Assert.AreEqual(1, queue.Count(), $"Only {maxCommandsCount} commands following SoftStopCommand must be processed");
        var expectedLog = new []{"Command 1", "Command 2", "Command 3", "Command 4", "Command 5"};
        CollectionAssert.AreEqual(expectedLog, log, "Incorrect processing log");
    }

    [TestMethod]
    public void MoveToCommand()
    {
        // Arrange
        var queue = new BlockingCollectionQueue();
        var newQueue = new BlockingCollectionQueue();
        var myThread = new MyThread(queue, Behaviours.Run);
        var resetEvent = new ManualResetEvent(false);
        var log = new List<string>();
        queue.Add(new WriteMessageCommand(log.Add, "Command 1"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 2"));
        queue.Add(new MacroCommand(new MoveToCommand(myThread, newQueue), new ActionCommand(() => resetEvent.Set())));
        queue.Add(new WriteMessageCommand(log.Add, "Command 3"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 4"));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        resetEvent.WaitOne();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        Assert.IsTrue(queue.IsEmpty(), "Queue must be empty");
        Assert.AreEqual(2, newQueue.Count(), "Commands following MoveToCommand must be moved to newQueue");
        var expectedLog = new []{"Command 1", "Command 2"};
        CollectionAssert.AreEqual(expectedLog, log, "Incorrect processing log");
    }

    [TestMethod]
    public void RunCommand()
    {
        // Arrange
        var maxCommandsCount = 2;
        var queue = new BlockingCollectionQueue();
        var myThread = new MyThread(queue, Behaviours.Run);
        var resetEvent = new ManualResetEvent(false);
        var log = new List<string>();
        queue.Add(new WriteMessageCommand(log.Add, "Command 1"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 2"));
        queue.Add(new SoftStopCommand(myThread, maxCommandsCount)); // process 2 next commands only
        queue.Add(new WriteMessageCommand(log.Add, "Command 3"));
        queue.Add(new MacroCommand(new RunCommand(myThread), new ActionCommand(() => resetEvent.Set()))); // process till stop command received
        queue.Add(new WriteMessageCommand(log.Add, "Command 4"));
        queue.Add(new WriteMessageCommand(log.Add, "Command 5"));
        queue.Add(new HardStopCommand(myThread));
        var startCommand = new StartCommand(myThread);
        
        // Act
        startCommand.Execute();
        resetEvent.WaitOne();
        myThread.Thread.Join(); // wait for myThread complete 

        // Assert
        Assert.IsTrue(queue.IsEmpty(), "All commands must be processed");
        var expectedLog = new []{"Command 1", "Command 2", "Command 3", "Command 4", "Command 5"};
        CollectionAssert.AreEqual(expectedLog, log, "Incorrect processing log");
    }
}