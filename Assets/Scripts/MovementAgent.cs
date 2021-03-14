using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MovementAgent : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    // TODO move somewhere???
    [SerializeField] private float _ampY;
    [SerializeField] private float _ampZ;
    [SerializeField] private float _legSpeed;

    private LegController _lc;

    private Vector3 _target;

    private bool _idle;

    


    private void Start()
    {
        _lc = new LegController(transform, _legSpeed, _ampY, _ampZ);
        _idle = true;
    }

    private void FixedUpdate()
    {
        if (_idle) return;

        Vector3 diff = _target - transform.position;
        if (diff.magnitude <= Time.deltaTime * _speed)
        {
            // Reached target
            MoveOn(diff);
            _lc.ResetLegs();
            _idle = true;
        }
        else
        {
            // Move to target in an ordinary fashion
            MoveOn(diff.normalized * (Time.deltaTime * _speed));
        }
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
        _idle = false;
    }

    private void MoveOn(Vector3 dx)
    {
        transform.position += dx;
        var angle = Vector3.Angle(dx, Vector3.forward);
        transform.eulerAngles = new Vector3(0, dx.x < 0 ? 360 - angle : angle, 0); // bodge
        _lc.MoveLegs();
    }

}