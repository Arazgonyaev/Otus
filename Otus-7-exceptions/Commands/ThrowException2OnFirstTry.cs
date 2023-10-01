namespace Otus_7_exceptions;

public class ThrowException2OnFirstTry : ICommand
{
    private int tryCount;
    
    public void Execute()
    {
        if (++tryCount == 1)
            throw new Exception2($"Command ThrowException1OnFirstTry executed first time");
    }
}
