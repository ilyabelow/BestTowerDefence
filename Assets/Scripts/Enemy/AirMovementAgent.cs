using Field;
using UnityEngine;
using Grid = Field.Grid;
using Vector3 = UnityEngine.Vector3;

namespace Enemy
{
    public class AirMovementAgent : IMovementAgent
    {
        private readonly EnemyData _data;
        private readonly Grid _grid;
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly Node _targetNode;
        private Node _occupiedNode;


        public AirMovementAgent(EnemyData data, Grid grid, Transform transform, float speed)
        {
            _targetNode = grid.TargetNode;
            _data = data;
            _grid = grid;
            _transform = transform;
            _speed = speed;
            _transform.position = grid.StartNode.Position;
            _occupiedNode = grid.StartNode;
        }


        public void TickMovement()
        {
            Vector3 diff = _targetNode.Position - _transform.position; 
            if (diff.magnitude <= Time.deltaTime * _speed)
            {
                // Reached target
                MoveOn(diff);
                _data.IsAlive = false;
            }
            else
            {
                // Move to target in an ordinary fashion
                MoveOn(diff.normalized * (Time.deltaTime * _speed));
            }

            var nodeUnder = _grid.GetNodeAtPoint(_transform.position);
            if (nodeUnder != _occupiedNode)
            {
                _occupiedNode.EnemyLeft(_data);
                nodeUnder.EnemyEntered(_data);
                _occupiedNode = nodeUnder;
            }

        }
        private void MoveOn(Vector3 dx)
        {
            _transform.position += dx;
            var angle = Vector3.Angle(dx, Vector3.forward);
            _transform.eulerAngles = new Vector3(0, dx.x < 0 ? 360 - angle : angle, 0); // bodge
        }
    }
}