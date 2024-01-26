namespace Otus_project_server
{
    public class MovableAdapter : IMovable
    {
        private readonly IUObject _uObject;
        
        public MovableAdapter(IUObject uObject)
        {
            _uObject = uObject;
        }

        public string Id => GetPropertyValue<string>("Id");

        public int[] Position {
            get => GetPropertyValue<int[]>("Position");
            set => _uObject.SetProperty("Position", value);
        }
        public int[] Velocity {
            get => GetPropertyValue<int[]>("Velocity");
            set => _uObject.SetProperty("Velocity", value);
        }
        
        public int[] PreviousPosition { 
            get => GetPropertyValue<int[]>("PreviousPosition");
            set => _uObject.SetProperty("PreviousPosition", value);
        }

        private T GetPropertyValue<T>(string propertyName)
        {
            var value = _uObject.GetProperty(propertyName);
        
            if (value is T correctValue)
                return correctValue;
            
            throw new ArgumentException($"Incorrect property value type for {propertyName}");
        }
    }
}
