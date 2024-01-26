namespace Otus_project_server;

public class ListLogger : ILogger
{
    private readonly List<string> log;

    public ListLogger(List<string> log)
    {
        this.log = log;
    }

    public void Log(string message)
    {
        log.Add(message);
    }
}
