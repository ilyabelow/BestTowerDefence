using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;

public class Gravity : MonoBehaviour
{
    [SerializeField] 
    private float massRatio;
    [SerializeField]
    private float gravityConstant;
    [SerializeField]
    private Vector3 startVelocity;

    private Vector3 velocity;

    private GameObject sun;
    
    private void Start()
    {
        velocity = startVelocity;
        sun = GameObject.Find("/GravityTask/sun");
        

    }

    void FixedUpdate()
    {
        Vector3 direction =  sun.transform.position - transform.position;
        float Rr = direction.magnitude;

        if (Rr < (transform.localScale.x + sun.transform.localScale.x)*.5) // Collision
        {
            Destroy(gameObject);
            Debug.Log("Boom!");
            return;
        }
        
        Vector3 force = gravityConstant * massRatio / (Rr * Rr * Rr) * direction;

        velocity += force * Time.deltaTime;
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
