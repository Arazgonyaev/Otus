namespace Otus_project_server
{
    public interface IUObject
    {
        object GetProperty(string name);
        void SetProperty(string name, object value);
        string Stringify();
    }
}
