using Field;
using UnityEngine;
using Grid = Field.Grid;
using Vector3 = UnityEngine.Vector3;

namespace Enemy.MovementAgent
{
    public class GridMovementAgent : IMovementAgent
    {
        private readonly EnemyView _view;
        
        private Node _targetNode;
        private readonly float _speed;


        public GridMovementAgent(EnemyView view, Grid grid, float speed)
        {
            _targetNode = grid.StartNode.NextNode;
            _view = view;
            _speed = speed;
            _view.transform.position = grid.StartNode.Position;
        }

        public void TickMovement()
        {
            Vector3 diff = _targetNode.Position - _view.transform.position;
            if (diff.magnitude <= Time.deltaTime * _speed)
            {
                MoveOn(diff);
                _targetNode.EnemyLeft(_view.Data);
                _targetNode = _targetNode.NextNode;
                if (_targetNode == null)
                {
                    _view.Data.Die();
                }
                else
                {
                    _targetNode.EnemyEntered(_view.Data);
                }
            }
            else
            {
                MoveOn(diff.normalized * (Time.deltaTime * _speed));
            }
        }
        private void MoveOn(Vector3 dx)
        {
            _view.transform.position += dx;
            _view.transform.rotation =  Quaternion.LookRotation(dx);
        }
    }
}