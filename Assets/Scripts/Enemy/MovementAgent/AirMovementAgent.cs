using Field;
using UnityEngine;
using Grid = Field.Grid;
using Vector3 = UnityEngine.Vector3;

namespace Enemy.MovementAgent
{
    public class AirMovementAgent : IMovementAgent
    {
        private readonly EnemyView _view;

        private readonly Grid _grid;
        private readonly Node _targetNode;
        private Node _occupiedNode;
        
        private readonly float _speed;


        public AirMovementAgent(EnemyView view, Grid grid, float speed)
        {
            _view = view;
            _grid = grid;
            _speed = speed;
            
            _view.transform.position = grid.StartNode.Position;
            _occupiedNode = grid.StartNode;
            _targetNode = grid.TargetNode;
        }


        public void TickMovement()
        {
            Vector3 diff = _targetNode.Position - _view.transform.position; 
            if (diff.magnitude <= Time.deltaTime * _speed)
            {
                MoveOn(diff);
                _view.Data.Die();
            }
            else
            {
                MoveOn(diff.normalized * (Time.deltaTime * _speed));
            }

            var nodeUnder = _grid.GetNodeAtPoint(_view.transform.position);
            if (nodeUnder != _occupiedNode)
            {
                _occupiedNode.EnemyLeft(_view.Data);
                nodeUnder.EnemyEntered(_view.Data);
                _occupiedNode = nodeUnder;
            }

        }
        private void MoveOn(Vector3 dx)
        {
            _view.transform.position += dx;
            _view.transform.LookAt(dx);
        }
    }
}