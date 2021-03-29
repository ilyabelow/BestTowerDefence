using Field;
using UnityEngine;
using Grid = Field.Grid;
using Vector3 = UnityEngine.Vector3;

namespace Enemy
{
    public class AirMovementAgent : IMovementAgent
    {
        private readonly Transform _transform;
        private readonly float _speed;
        private Node _targetNode;


        public AirMovementAgent(Grid grid, Transform transform, float speed)
        {
            _targetNode = grid.TargetNode;
            _transform = transform;
            _speed = speed;
            _transform.position = grid.StartNode.Position;
        }


        public void TickMovement()
        {
            Vector3 diff = _targetNode.Position - _transform.position;
            if (diff.magnitude <= Time.deltaTime * _speed)
            {
                // Reached target
                MoveOn(diff);
                _targetNode = null;
            }
            else
            {
                // Move to target in an ordinary fashion
                MoveOn(diff.normalized * (Time.deltaTime * _speed));
            }
        }

        public bool ReachedGoal()
        {
            return _targetNode == null;
        }

        private void MoveOn(Vector3 dx)
        {
            _transform.position += dx;
            var angle = Vector3.Angle(dx, Vector3.forward);
            _transform.eulerAngles = new Vector3(0, dx.x < 0 ? 360 - angle : angle, 0); // bodge
        }
    }
}