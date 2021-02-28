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
    [SerializeField] private GameObject _cursor;

    [SerializeField] private MovementMode _mode;

    private Camera _camera;
    private Vector3 _offset;

    // Temp solution, TODO introduce support of arbitrary plane orientation
    private readonly Vector3 _iVector = Vector3.right;
    private readonly Vector3 _jVector = Vector3.forward;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform != transform) // temp solution?
            {
                return;
            }

            _cursor.SetActive(true);
            if (_mode is MovementMode.Free)
            {
                _cursor.transform.position = hit.point;
            }
            else if (_mode is MovementMode.Grid)
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
        float width = _gridWidth * _nodeSize;
        float height = _gridHeight * _nodeSize;

        // Default plane size is 10 by 10 (bodge)
        transform.localScale = new Vector3(width * 0.1f, 1f, height * 0.1f);
        _offset = transform.position - 0.5f * new Vector3(width, 0f, height);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        // Grid drawing
        for (int i = 1; i < _gridWidth; i++)
        {
            Gizmos.DrawLine(_offset + i * _iVector, _offset + i * _iVector + _gridHeight * _jVector);
        }

        for (int j = 1; j < _gridHeight; j++)
        {
            Gizmos.DrawLine(_offset + j * _jVector, _offset + j * _jVector + _gridWidth * _iVector);
        }

        // Origin drawing
        Gizmos.DrawSphere(_offset, 0.1f);
    }
}