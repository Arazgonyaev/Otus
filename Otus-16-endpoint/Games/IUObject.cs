namespace Otus_16_endpoint
{
    public interface IUObject
    {
        object GetProperty(string name);
        void SetProperty(string name, object value);
        string Stringify();
    }
}
