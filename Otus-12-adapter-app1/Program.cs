using System;
using Otus_12_adapter_app1;

internal class Program
{
    private static void Main(string[] args)
    {
        Args argsObj = ParseArgsExitOnIncorrect(args);

        IMatrixSummator summator = new MatrixSummator();

        summator.ReadMatricesFromFile(argsObj.InputFileName);
        summator.Sum();
        summator.SaveResultMatrixToFile(argsObj.OutputFileName);
    }

    static Args ParseArgsExitOnIncorrect(string[] args)
    {
        if (args.Length == 2)
            return new Args(args[0], args[1]);
            
        Console.WriteLine("Two arguments must be provided: " + 
            "input file name, output file name.");
        
        Environment.Exit(0);
        return new Args(null, null); // fake return
    }

    public record Args(string InputFileName, string OutputFileName);
}