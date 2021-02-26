using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;

public class GravityAgent : MonoBehaviour
{
    [SerializeField] 
    private float mass;
    [SerializeField] 
    private float gravityConstant; // TODO make common
    [SerializeField]
    private Vector3 startVelocity;
    [SerializeField]
    private GravityAgent companion;
    
    private Vector3 velocity;
    

    
    private void Start()
    {
        velocity = startVelocity;
    }

    void FixedUpdate()
    {
        Vector3 direction =  companion.transform.position - transform.position;
        float Rr = direction.magnitude;

        if (Rr < (transform.localScale.x + companion.transform.localScale.x)*.5) // Collision
        {
            Destroy(gameObject);
            Debug.Log("Boom!");
            return;
        }
        
        Vector3 acc = gravityConstant * companion.mass / (mass * Rr * Rr * Rr) * direction;

        velocity += acc * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position+startVelocity);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + velocity);
        }
    }
}
