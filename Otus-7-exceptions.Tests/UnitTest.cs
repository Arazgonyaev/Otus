using System.Collections.Concurrent;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static Otus_7_exceptions.ExceptionHandler;

namespace Otus_7_exceptions.Tests;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public void TestPoint4_WriteLog()
    {
        Mock<ICommand> commandMock = new Mock<ICommand>();
        Mock<ILogger> loggerMock = new Mock<ILogger>();
        
        var handlerCommand = WriteLog(loggerMock.Object);
        // Execute writing to log
        handlerCommand(commandMock.Object, new BaseException("Test message")).Execute();
        
        Assert.AreEqual(1, loggerMock.Invocations.Count, "Incorrect logger invocations count");
        Assert.AreEqual("ERROR while executing command ICommandProxy: Test message", 
            loggerMock.Invocations.Single().Arguments.Single().ToString(), 
            "Incorrect log message");
    }

    [TestMethod]
    public void TestPoint5_QueueWriteLog()
    {
        Mock<ICommand> commandMock = new Mock<ICommand>();
        Mock<ILogger> loggerMock = new Mock<ILogger>();
        var queue = new BlockingCollection<ICommand>();
        
        var handlerCommand = QueueWriteLog(loggerMock.Object, queue);
        // Execute queueing writing to log
        handlerCommand(commandMock.Object, new BaseException("Test message")).Execute();
        
        Assert.AreEqual(0, loggerMock.Invocations.Count, "Incorrect logger invocations count");
        Assert.AreEqual(1, queue.Count, "Incorrect queue items count");
        
        // Execute writing to log
        queue.Take().Execute();

        Assert.AreEqual(1, loggerMock.Invocations.Count, "Incorrect logger invocations count");
        Assert.AreEqual("ERROR while executing command ICommandProxy: Test message", 
            loggerMock.Invocations.Single().Arguments.Single().ToString(), 
            "Incorrect log message");
        Assert.AreEqual(0, queue.Count, "Incorrect queue items count after processing");
    }

    [TestMethod]
    public void TestPoint6_QueueCommand()
    {
        Mock<ICommand> commandMock = new Mock<ICommand>();
        var queue = new BlockingCollection<ICommand>();
        
        var handlerCommand = QueueCommand(queue);
        // Execute queueing command
        handlerCommand(commandMock.Object, new BaseException("Test message")).Execute();
        
        Assert.AreEqual(1, queue.Count, "Incorrect queue items count");
        
        // Execute command
        queue.Take().Execute();

        Assert.AreEqual(0, queue.Count, "Incorrect queue items count after processing");
    }

    [TestMethod]
    public void TestPoint7_QueueQueueCommand()
    {
        Mock<ICommand> commandMock = new Mock<ICommand>();
        var queue = new BlockingCollection<ICommand>();
        
        var handlerCommand = QueueQueueCommand(queue);
        // Execute queueing queueing command
        handlerCommand(commandMock.Object, new BaseException("Test message")).Execute();
        
        Assert.AreEqual(1, queue.Count, "Incorrect queue items count");
        
        // Execute queueing command
        queue.Take().Execute();

        Assert.AreEqual(1, queue.Count, "Incorrect queue items count");
        
        // Execute command
        queue.Take().Execute();

        Assert.AreEqual(0, queue.Count, "Incorrect queue items count after processing");
    }

    [TestMethod]
    public void TestPoint8_RepeatCommandOrWriteLog_2attempts()
    {
        var command = new RepeatableCommand(new ThrowException1("Step 8"), 2);
        Mock<ILogger> loggerMock = new Mock<ILogger>();
        var queue = new BlockingCollection<ICommand>();
        
        // Execute command for getting first error
        Assert.ThrowsException<Exception1>(() => command.Execute());

        var handlerCommand = RepeatCommandOrWriteLog(loggerMock.Object, queue);
        // Execute queueing command after first error
        handlerCommand(command, new BaseException("Error 1")).Execute();
        
        Assert.AreEqual(0, loggerMock.Invocations.Count, "Incorrect logger invocations count");
        Assert.AreEqual(1, queue.Count, "Incorrect queue items count");
        
        command = (RepeatableCommand)queue.Take();
        // Execute command for getting second error
        Assert.ThrowsException<Exception1>(() => command.Execute());

        // Execute writing to log
        handlerCommand(command, new BaseException("Error 2")).Execute();
        
        Assert.AreEqual(1, loggerMock.Invocations.Count, "Incorrect logger invocations count");
        Assert.AreEqual("Repeatable command is out of try count or the command is not repeatable. Last error: Error 2", 
            loggerMock.Invocations.Single().Arguments.Single().ToString(), 
            "Incorrect log message");
        Assert.AreEqual(0, queue.Count, "Incorrect queue items count after processing");
    }

    [TestMethod]
    public void TestPoint9_RepeatCommandOrWriteLog_3attempts()
    {
        var command = new RepeatableCommand(new ThrowException1("Step 9"), 3);
        Mock<ILogger> loggerMock = new Mock<ILogger>();
        var queue = new BlockingCollection<ICommand>();
        
        // Execute command for getting first error
        Assert.ThrowsException<Exception1>(() => command.Execute());

        var handlerCommand = RepeatCommandOrWriteLog(loggerMock.Object, queue);
        // Execute queueing command after first error
        handlerCommand(command, new BaseException("Error 1")).Execute();
        
        Assert.AreEqual(0, loggerMock.Invocations.Count, "Incorrect logger invocations count");
        Assert.AreEqual(1, queue.Count, "Incorrect queue items count");
        
        command = (RepeatableCommand)queue.Take();
        // Execute command for getting second error
        Assert.ThrowsException<Exception1>(() => command.Execute());

        // Execute queueing command after second error
        handlerCommand(command, new BaseException("Error 2")).Execute();
        
        Assert.AreEqual(0, loggerMock.Invocations.Count, "Incorrect logger invocations count");
        Assert.AreEqual(1, queue.Count, "Incorrect queue items count");
        
        command = (RepeatableCommand)queue.Take();
        // Execute command for getting third error
        Assert.ThrowsException<Exception1>(() => command.Execute());

        // Execute writing to log
        handlerCommand(command, new BaseException("Error 3")).Execute();
        
        Assert.AreEqual(1, loggerMock.Invocations.Count, "Incorrect logger invocations count");
        Assert.AreEqual("Repeatable command is out of try count or the command is not repeatable. Last error: Error 3", 
            loggerMock.Invocations.Single().Arguments.Single().ToString(), 
            "Incorrect log message");
        Assert.AreEqual(0, queue.Count, "Incorrect queue items count after processing");
    }
}