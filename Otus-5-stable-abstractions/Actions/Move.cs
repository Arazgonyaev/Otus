using Otus_5_stable_abstractions.Interfaces;

namespace Otus_5_stable_abstractions.Actions
{
    public class Move
    {
        readonly IMovable _movable;
        
        public Move(IMovable movable)
        {
            _movable = movable;
        }

        public void Execute()
        {
            _movable.Position = _movable.Position.Plus(_movable.Velocity);
        }
    }
}
