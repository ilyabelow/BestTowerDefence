using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MovementAgent : MonoBehaviour
{
    [SerializeField]
    private float time;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int inSin;
    [SerializeField]
    private int inCos;
    [SerializeField]
    private float deltaPhi;
    
    private float _curTime;

    void FixedUpdate()
    {
        // Lissajous curve drawing
        Vector3 dir = Vector3.zero;
        float phi = _curTime / time * 2 * (float) Math.PI;
        dir.x = -inCos*Mathf.Sin(inCos*phi); // derivatives
        dir.z = inSin*Mathf.Cos(inSin*phi+deltaPhi);
        transform.position += dir * speed;
        
        // Timekeeping
        _curTime += Time.deltaTime;
        if (_curTime > time)
        {
            _curTime = 0;
        }
        
        // Character rotation so she faces to dir vector
        var angle = Vector3.Angle(dir, Vector3.forward);
        transform.eulerAngles = new Vector3(0, dir.x < 0 ? 360 - angle: angle, 0);
        
        // Leg movement
        transform.Find("leg1").transform.localPosition = Vector3.up * (0.1f * Mathf.Sin(10*phi)) + new Vector3(0.25f, -1.5f, 0f);
        transform.Find("leg2").transform.localPosition = Vector3.up * (0.1f * Mathf.Sin(10*phi + 3.14f)) + new Vector3(-0.25f, -1.5f, 0f);
    }
    
    
}
