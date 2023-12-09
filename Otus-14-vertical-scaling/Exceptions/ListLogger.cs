using System.Collections.Generic;

namespace Otus_14_vertical_scaling;

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
