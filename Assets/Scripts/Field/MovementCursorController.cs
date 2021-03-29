using Runtime;

namespace Field
{
    public class MovementCursorController : IController
    {
        private MovementCursor _movementCursor;

        public MovementCursorController(MovementCursor movementCursor)
        {
            _movementCursor = movementCursor;
        }

        public void Tick()
        {
            _movementCursor.Raycast();
        }

        public void OnStart()
        {
        }

        public void OnStop()
        {
        }
    }
}