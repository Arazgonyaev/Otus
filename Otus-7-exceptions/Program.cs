using System.Collections.Concurrent;
using static Otus_7_exceptions.ExceptionHandler;

namespace Otus_7_exceptions;

class Program
{
    static void Main(string[] args)
    {
        BlockingCollection<ICommand> queue = new();
        ILogger logger = new ConsoleLogger();

        // Регистрируем обработчики, необходимые для последующих команд
        RegisterHandler(typeof(ThrowException1), typeof(Exception1), WriteLog(logger));
        RegisterHandler(typeof(ThrowException2), typeof(Exception2), QueueWriteLog(logger, queue));
        RegisterHandler(typeof(ThrowException1OnFirstTry), typeof(BaseException), QueueCommand(queue));
        RegisterHandler(typeof(ThrowException2OnFirstTry), typeof(BaseException), QueueQueueCommand(queue));
        RegisterHandler(typeof(RepeatableCommand), typeof(BaseException), RepeatCommandOrWriteLog(logger, queue));
        
        // Добавим в очередь команду, не вызывающую исключение.
        queue.Add(new WriteMessageToLog(logger, "First message"));
        
        // Дальше демонстрируется выполнение каждого пункта ДЗ:

        // 4. Реализовать Команду, которая записывает информацию о выброшенном исключении в лог.
        queue.Add(new ThrowException1("Step 4"));
        
        // 5. Реализовать обработчик исключения, который ставит Команду, пишущую в лог в очередь Команд.
        queue.Add(new ThrowException2("Step 5"));

        // 6. Реализовать Команду, которая повторяет Команду, выбросившую исключение.
        queue.Add(new ThrowException1OnFirstTry());

        // 7. Реализовать обработчик исключения, который ставит в очередь Команду - повторитель команды, выбросившей исключение.
        queue.Add(new ThrowException2OnFirstTry());

        // 8. .. при первом выбросе исключения повторить команду, при повторном выбросе исключения записать информацию в лог.
        queue.Add(new RepeatableCommand(new ThrowException1("Step 8"), 2));
        
        // 9. .. повторить два раза, потом записать в лог.
        queue.Add(new RepeatableCommand(new ThrowException2("Step 9"), 3));

        // Запускаем обработку очереди команд.
        QueueProcessor.ProcessWhileNotEmpty(queue);
    }
}
