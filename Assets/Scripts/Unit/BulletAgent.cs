using UnityEngine;

public class BulletAgent : MonoBehaviour
{

    private Vector3 _velocity;
    private float _timeLeft;
    

    public void Constructor(Vector3 velocity, float timeLeft)
    {
        _velocity = velocity;
        _timeLeft = timeLeft;
    }
    
    void Update()
    {
        transform.position += _velocity * Time.deltaTime;
        _timeLeft -= Time.deltaTime;
        if (_timeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }
}
