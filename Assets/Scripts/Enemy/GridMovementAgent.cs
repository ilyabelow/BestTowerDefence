using Field;
using UnityEngine;
using Grid = Field.Grid;
using Vector3 = UnityEngine.Vector3;

namespace Enemy
{
    public class GridMovementAgent : IMovementAgent
    {
        // private float _ampY = 0.1f;
        // private float _ampZ = 0.1f;
        // private float _legSpeed = 1;
        // private float _legTime;
        // private Transform _leg1;
        // private Transform _leg2;

        private readonly Transform _transform;
        private readonly float _speed;
        private Node _targetNode;


        public GridMovementAgent(Grid grid, Transform transform, float speed)
        {
            _targetNode = grid.StartNode.NextNode;
            _transform = transform;
            _speed = speed;
            _transform.position = grid.StartNode.Position;
            // _leg1 = transform.Find("leg1_container/leg1");
            // _leg2 = transform.Find("leg2_container/leg2");
        }

        public void TickMovement()
        {
            Vector3 diff = _targetNode.Position - _transform.position;
            if (diff.magnitude <= Time.deltaTime * _speed)
            {
                // Reached target
                MoveOn(diff);
                _targetNode = _targetNode.NextNode;
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
            //    MoveLegs();
        }

        // TODO animate
        // private void MoveLegs()
        // {
        //     _legTime += _legSpeed;
        //     if (_legTime > 2)
        //     {
        //         _legTime = 0;
        //     }
        //
        //     _leg1.localPosition = _ampY * Mathf.Sin(_legTime * (float) Math.PI) * Vector3.up
        //                           + _ampZ * Mathf.Cos(_legTime * (float) Math.PI) * Vector3.back;
        //     _leg2.localPosition = _ampY * Mathf.Sin(_legTime * (float) Math.PI) * Vector3.down
        //                           + _ampZ * Mathf.Cos(_legTime * (float) Math.PI) * Vector3.forward;
        // }
    }
}