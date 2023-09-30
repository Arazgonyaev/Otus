namespace Otus_7_exceptions;

public class ThrowException1 : ICommand
{
    private readonly string message;

    public ThrowException1(string message)
    {
        this.message = message;
    }
    
    public void Execute()
    {
        throw new Exception1($"Error in command ThrowException1. Message: {message}");
    }
}
