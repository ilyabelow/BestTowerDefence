using Field;
using UnityEngine;
using Grid = Field.Grid;
using Vector3 = UnityEngine.Vector3;

namespace Enemy
{
    public class GridMovementAgent : IMovementAgent
    {
        private readonly EnemyData _data;
        private readonly Transform _transform;
        private readonly float _speed;
        private Node _targetNode;


        public GridMovementAgent(EnemyData data, Grid grid, Transform transform, float speed)
        {
            _targetNode = grid.StartNode.NextNode;
            _data = data;
            _transform = transform;
            _speed = speed;
            _transform.position = grid.StartNode.Position;
        }

        public void TickMovement()
        {
            Vector3 diff = _targetNode.Position - _transform.position;
            if (diff.magnitude <= Time.deltaTime * _speed)
            {
                MoveOn(diff);
                _targetNode.EnemyLeft(_data);
                _targetNode = _targetNode.NextNode;
                if (_targetNode == null)
                {
                    _data.IsAlive = false;
                }
                else
                {
                    _targetNode.EnemyEntered(_data);
                }
            }
            else
            {
                MoveOn(diff.normalized * (Time.deltaTime * _speed));
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