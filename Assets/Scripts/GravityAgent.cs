using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;

public class GravityAgent : MonoBehaviour
{
    [SerializeField] 
    private float _mass;
    [SerializeField] 
    private float _gravityConstant; // TODO make common
    [SerializeField]
    private Vector3 _startVelocity;
    [SerializeField]
    private GravityAgent _companion;
    
    private Vector3 _velocity;
    

    
    private void Start()
    {
        _velocity = _startVelocity;
    }

    void FixedUpdate()
    {
        Vector3 difference =  _companion.transform.position - transform.position;
        float rr = difference.magnitude;

        if (rr < (transform.localScale.x + _companion.transform.localScale.x)*.5) // Collision
        {
            gameObject.SetActive(false);
            _companion.gameObject.SetActive(false);
            Debug.Log("Boom!");
            return;
        }
        
        Vector3 acc = _gravityConstant * _companion._mass / (rr * rr * rr) * difference;
        
        _velocity += Time.deltaTime * acc;
        transform.position += Time.deltaTime * _velocity;

    }

    private void OnDrawGizmos()
    {
        var position = transform.position;
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(position, position+_startVelocity);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(position, position + _velocity);
        }
    }

}
