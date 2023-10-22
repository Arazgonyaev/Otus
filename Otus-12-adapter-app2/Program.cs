using System;
using Otus_12_adapter_app2;

internal class Program
{
    private static void Main(string[] args)
    {
        Args argsObj = ParseArgsExitOnIncorrect(args);

        IMatrixGenerator generator = new MatrixGenerator();

        generator.Generate(2);
        generator.SaveMatricesToFile(argsObj.GeneratedMatricesFileName);

        if (!string.IsNullOrEmpty(argsObj.SumMatrixFileName))
        {
            ISummator adaptor = new MatrixSummatorAdaptor();
            adaptor.Sum(argsObj.GeneratedMatricesFileName, argsObj.SumMatrixFileName);
        }
    }

    static Args ParseArgsExitOnIncorrect(string[] args)
    {
        if (args.Length == 2)
            return new Args(args[0], args[1]);
            
        if (args.Length == 1)
            return new Args(args[0], null);
        
        Console.WriteLine("One or two arguments must be provided: " + 
            "generated matrices file name, [sum matrix file name].");
        
        Environment.Exit(0);
        return new Args(null, null); // fake return
    }

    public record Args(string GeneratedMatricesFileName, string SumMatrixFileName);
}