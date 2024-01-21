namespace Otus_project_server;

public class ShotableAdapter : IShotable
{
    private readonly IUObject _uObject;
        
    public ShotableAdapter(IUObject uObject)
    {
        _uObject = uObject;
    }

    public string Id => GetPropertyValue<string>("Id");

    public int ShellCnt {
        get => GetPropertyValue<int>("ShellCnt");
        set => _uObject.SetProperty("ShellCnt", value);
    }
    
    private T GetPropertyValue<T>(string propertyName)
    {
        var value = _uObject.GetProperty(propertyName);
    
        if (value is T correctValue)
            return correctValue;
        
        throw new ArgumentException($"Incorrect property value type for {propertyName}");
    }
}
