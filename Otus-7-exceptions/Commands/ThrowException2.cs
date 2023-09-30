namespace Otus_7_exceptions;

public class ThrowException2 : ICommand
{
    private readonly string message;

    public ThrowException2(string message)
    {
        this.message = message;
    }
    
    public void Execute()
    {
        throw new Exception2($"Error in command ThrowException2. Message: {message}");
    }
}
