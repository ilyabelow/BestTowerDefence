using System;
using UnityEngine;

internal enum MovementMode
{
    Free,
    Grid,
}

public class MovementCursor : MonoBehaviour
{
    // Grid params
    [SerializeField] private uint _gridWidth;
    [SerializeField] private uint _gridHeight;
    [SerializeField] private float _nodeSize;

    [SerializeField] private MovementAgent _agent;
    [SerializeField] private GameObject _cursorPrefab;

    [SerializeField] private MovementMode _mode;
    
    private GameObject _cursor;

    private Camera _camera;
    private Vector3 _offset;

    // Temp solution, TODO introduce support of arbitrary plane orientation
    private readonly Vector3 _iVector = Vector3.right;
    private readonly Vector3 _jVector = Vector3.forward;

    private void Awake()
    {
        _camera = Camera.main;
        ResizePlane();
        _cursor = Instantiate(_cursorPrefab, Vector3.zero, Quaternion.identity);
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform != transform) // temp solution?
            {
                // TODO plane behind agent is unreachable, that's bad
                _cursor.SetActive(false); //disable if ray did not reach the plane
                return;
            }

            _cursor.SetActive(true);
            if (_mode == MovementMode.Free)
            {
                _cursor.transform.position = hit.point;
            }
            else if (_mode == MovementMode.Grid)
            {
                Vector3 difference = hit.point - _offset;
                int x = (int) (difference.x / _nodeSize);
                int z = (int) (difference.z / _nodeSize);
                _cursor.transform.position = _offset + _iVector * x + _jVector * z + 0.5f * (_iVector + _jVector);
            }

            if (Input.GetMouseButton(0)) // Use GetMouseButton instead of GetMouseButtonDown to enable dragging
            {
                _agent.SetTarget(_cursor.transform.position);
            }
        }
        else
        {
            _cursor.SetActive(false); //disable if cursor is not on the plane
        }
    }

    private void OnValidate()
    {
        ResizePlane();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        // Grid drawing
        for (int i = 1; i < _gridWidth; i++)
        {
            Gizmos.DrawLine(_offset + i * _iVector * _nodeSize, _offset + i * _iVector * _nodeSize + _gridHeight * _jVector * _nodeSize);
        }

        for (int j = 1; j < _gridHeight; j++)
        {
            Gizmos.DrawLine(_offset + j * _jVector * _nodeSize, _offset + j * _jVector * _nodeSize + _gridWidth * _iVector * _nodeSize);
        }

        // Origin drawing
        Gizmos.DrawSphere(_offset, 0.1f);
    }

    private void ResizePlane()
    {
        float width = _gridWidth * _nodeSize;
        float height = _gridHeight * _nodeSize;

        // Default plane size is 10 by 10 (bodge)
        transform.localScale = new Vector3(width * 0.1f, 1f, height * 0.1f);
        _offset = transform.position - 0.5f * new Vector3(width, 0f, height);
    }
}

public class LegController
{
    private Transform _leg1;
    private Transform _leg2;

    private float _legTime;
    private float _inc;

    private float _ampZ;
    private float _ampY;


    public LegController(Transform parent, float inc, float ampY, float ampZ)
    {
        _inc = inc;
        _ampZ = ampZ;
        _ampY = ampY;
        _leg1 = parent.Find("leg1_container/leg1");
        _leg2 = parent.Find("leg2_container/leg2");
    }


    public void MoveLegs()
    {
        _legTime += _inc;
        if (_legTime > 2)
        {
            _legTime = 0;
        }

        _leg1.localPosition = _ampY * Mathf.Sin(_legTime * (float) Math.PI) * Vector3.up
                              + _ampZ * Mathf.Cos(_legTime* (float) Math.PI) * Vector3.back;
        _leg2.localPosition = _ampY * Mathf.Sin(_legTime * (float) Math.PI) * Vector3.down
                              + _ampZ * Mathf.Cos(_legTime * (float) Math.PI) * Vector3.forward;
    }

    public void ResetLegs()
    {
        _legTime = 0;
        _leg1.localPosition = Vector3.zero;
        _leg2.localPosition = Vector3.zero;
    }
}