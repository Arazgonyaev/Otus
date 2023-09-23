namespace Otus_5_stable_abstractions.Interfaces
{
    public interface IUObject
    {
        object GetProperty(string name);
        void SetProperty(string name, object value);
    }
}
