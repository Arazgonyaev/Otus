using System;
using Otus_5_stable_abstractions.Interfaces;

namespace Otus_5_stable_abstractions.Adapters
{
    public class MovableAdapter : IMovable
    {
        private readonly IUObject _uObject;
        
        public MovableAdapter(IUObject uObject)
        {
            _uObject = uObject;
        }

        public int[] Position {
            get => GetPropertyValue("Position");
            set => _uObject.SetProperty("Position", value);
        }
        public int[] Velocity => GetPropertyValue("Velocity");

        private int[] GetPropertyValue(string propertyName)
        {
            var value = _uObject.GetProperty(propertyName);
            
            if (value is int[] i)
                return i;
            
            throw new ArgumentException($"Incorrect property value type for {propertyName}");
        }
    }
}
