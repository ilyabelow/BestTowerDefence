using System;
using UnityEngine;

namespace Unit
{
    // TODO animate
    public class EnemySpawner : MonoBehaviour
    {

        private float _phase;
        private Transform _sphere;

        void Start()
        {
            _sphere = transform.Find("Sphere");
            _phase = 0;
        }

        void Update()
        {
            _sphere.localPosition = new Vector3(0, 0.5f*Mathf.Sin(_phase * (float) Math.PI) + 1f , 0);
            _phase += Time.deltaTime;
            _phase %= 2;
        }
    }
}
