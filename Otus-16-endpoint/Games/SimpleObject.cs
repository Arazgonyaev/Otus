namespace Otus_16_endpoint;

public class SimpleObject : IObject
{
    public string ObjectId { get; }
    public string ObjectState { get; set; }

    public SimpleObject(string objectId)
    {
        ObjectId = objectId;
    }
}
