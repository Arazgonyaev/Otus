namespace Otus_12_adapter_app1;

public interface IMatrixSummator
{
    void ReadMatricesFromFile(string fileName);
    void Sum();
    void SaveResultMatrixToFile(string fileName);
}
