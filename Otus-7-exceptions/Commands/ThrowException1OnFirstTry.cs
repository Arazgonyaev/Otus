namespace Otus_7_exceptions;

public class ThrowException1OnFirstTry : ICommand
{
    private int tryCount;
    
    public void Execute()
    {
        if (++tryCount == 1)
            throw new Exception1($"Command ThrowException1OnFirstTry executed first time");
    }
}
