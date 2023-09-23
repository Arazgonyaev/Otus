using Otus_5_stable_abstractions.Interfaces;

namespace Otus_5_stable_abstractions.Actions
{
    public class Rotate
    {
        readonly IRotatable _rotatable;
        
        public Rotate(IRotatable rotatable)
        {
            _rotatable = rotatable;
        }

        public void Execute()
        {
            _rotatable.Direction = (_rotatable.Direction + _rotatable.AngularVelocity) % _rotatable.DirectionsCount;
        }
    }
}
