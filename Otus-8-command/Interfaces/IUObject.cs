namespace Otus_8_command.Interfaces;

public interface IUObject
{
    object GetProperty(string name);
    void SetProperty(string name, object value);
}
