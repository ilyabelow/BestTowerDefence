using System;
using Field;
using UnityEngine;
using Object = System.Object;
using Vector3 = UnityEngine.Vector3;

public class MovementAgent : MonoBehaviour
{
    [SerializeField] private float _speed;

    // TODO move somewhere???
    [SerializeField] private float _ampY;
    [SerializeField] private float _ampZ;
    [SerializeField] private float _legSpeed;
    private float _legTime;

    private Transform _leg1;
    private Transform _leg2;
    
    private Node _targetNode;

    public Node TargetNode
    {
        set
        {
            if (value == null)
            {
                Destroy(this.gameObject);
            }
            _targetNode = value;
        }
    }
    

    private void Awake()
    {
        _leg1 = transform.Find("leg1_container/leg1");
        _leg2 = transform.Find("leg2_container/leg2");
    }

    private void FixedUpdate()
    {
        Vector3 diff = _targetNode.Position - transform.position;
        if (diff.magnitude <= Time.deltaTime * _speed)
        {
            // Reached target
            MoveOn(diff);
            TargetNode = _targetNode.NextNode;
        }
        else
        {
            // Move to target in an ordinary fashion
            MoveOn(diff.normalized * (Time.deltaTime * _speed));
        }
    }

    private void MoveOn(Vector3 dx)
    {
        transform.position += dx;
        var angle = Vector3.Angle(dx, Vector3.forward);
        transform.eulerAngles = new Vector3(0, dx.x < 0 ? 360 - angle : angle, 0); // bodge
        MoveLegs();
    }
    
    
    public void MoveLegs()
    {
        _legTime += _legSpeed;
        if (_legTime > 2)
        {
            _legTime = 0;
        }

        _leg1.localPosition = _ampY * Mathf.Sin(_legTime * (float) Math.PI) * Vector3.up
                              + _ampZ * Mathf.Cos(_legTime * (float) Math.PI) * Vector3.back;
        _leg2.localPosition = _ampY * Mathf.Sin(_legTime * (float) Math.PI) * Vector3.down
                              + _ampZ * Mathf.Cos(_legTime * (float) Math.PI) * Vector3.forward;
    }
}
