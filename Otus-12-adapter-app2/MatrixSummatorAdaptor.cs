using Otus_12_adapter_app1;

namespace Otus_12_adapter_app2;

public class MatrixSummatorAdaptor : ISummator
{
    public void Sum(string inputFileName, string outputFileName)
    {
        IMatrixSummator summator = new MatrixSummator();

        summator.ReadMatricesFromFile(inputFileName);
        summator.Sum();
        summator.SaveResultMatrixToFile(outputFileName);
    }
}
