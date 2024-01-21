namespace Otus_project_server;

public class HtttpNotifier : INotifier
{
    private readonly HttpClient client = new HttpClient();
    private readonly string url;

    public HtttpNotifier(string url)
    {
        this.url = url;
    }

    public void ObjectCreated(string objectId, int[] position)
    {
        Send("createObject", objectId, position);
    }

    public void ObjectMoved(string objectId, int[] position)
    {
        Send("moveObject", objectId, position);
    }

    public void ObjectsUpdated((string objectId, int[] position)[] objects)
    {
        SendBulk("updateObjects", objects);
    }

    private void Send(string command, string objectId, int[] position)
    {
        var values = new Dictionary<string, string>();
        values["id"] = objectId;
        values["x"] = position[0].ToString();
        values["y"] = position[1].ToString();
        
        JsonContent content = JsonContent.Create(values);

        client.PostAsync(url + '/' + command, content).Wait();
    }

    private void SendBulk(string command, (string objectId, int[] position)[] objects)
    {
        var valuesList = new List<Dictionary<string, string>>();
        foreach (var obj in objects)
        {
            var values = new Dictionary<string, string>();
            values["id"] = obj.objectId;
            values["x"] = obj.position[0].ToString();
            values["y"] = obj.position[1].ToString();
            valuesList.Add(values);
        }
        
        JsonContent content = JsonContent.Create(valuesList);

        client.PostAsync(url + '/' + command, content).Wait();
    }
}
