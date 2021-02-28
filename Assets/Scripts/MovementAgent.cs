using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MovementAgent : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _targetMarker;


    private float _legTime;

    private Vector3 _target;

    private bool _idle;

    private Transform _leg1;
    private Transform _leg2;

    private void Start()
    {
        _idle = true;
        _leg1 = transform.Find("leg1_container/leg1");
        _leg2 = transform.Find("leg2_container/leg2");
    }

    private void FixedUpdate()
    {
        if (_idle) return;

        Vector3 diff = _target - transform.position;
        if (diff.magnitude <= Time.deltaTime * _speed)
        {
            // Reached target
            MoveOn(diff);
            ResetLegs();

            _targetMarker.SetActive(false);
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
        _targetMarker.SetActive(true);
        _targetMarker.transform.position = target;
    }

    private void MoveOn(Vector3 dx)
    {
        transform.position += dx;
        var angle = Vector3.Angle(dx, Vector3.forward);
        transform.eulerAngles = new Vector3(0, dx.x < 0 ? 360 - angle : angle, 0); // bodge
        MoveLegs();
    }

    private void MoveLegs()
    {
        _legTime += 0.1f;
        if (_legTime > 2)
        {
            _legTime = 0;
        }

        _leg1.localPosition = 0.2f * Mathf.Sin(_legTime * (float) Math.PI) * Vector3.up;
        _leg2.localPosition = 0.2f * Mathf.Sin(_legTime * (float) Math.PI) * Vector3.down;
    }

    private void ResetLegs()
    {
        _legTime = 0;
        _leg1.localPosition = Vector3.zero;
        _leg2.localPosition = Vector3.zero;
    }
}