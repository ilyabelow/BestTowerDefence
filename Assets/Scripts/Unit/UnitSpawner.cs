using System;
using System.Collections;
using System.Collections.Generic;
using Field;
using UnityEngine;
using Object = System.Object;

public class UnitSpawner : MonoBehaviour
{

    [SerializeField] private float _spawnInterval;
    [SerializeField] private MovementAgent _unitPrefab;

    
    private float _phase;
    private float _timer;
    private Transform _sphere;

    public Node HomeNode { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _sphere = transform.Find("Sphere");
        _phase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _sphere.localPosition = new Vector3(0, 0.5f*Mathf.Sin(_phase * (float) Math.PI) + 1f , 0);
        _phase += Time.deltaTime;
        _phase %= 2;

        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            var unit = Instantiate(_unitPrefab, HomeNode.Position, Quaternion.identity);
            unit.TargetNode = HomeNode.NextNode;
            _timer = 0;
        }
    }
}
