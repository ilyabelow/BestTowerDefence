using System;
using Turret;
using Unit;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Field
{
    public class MovementCursor : MonoBehaviour
    {
        // Grid params
        [SerializeField] private uint _gridWidth;
        [SerializeField] private uint _gridHeight;
        [SerializeField] private float _nodeSize;

        [SerializeField] private GameObject _cursorPrefab;
        [SerializeField] private Material _cursorOk;
        [SerializeField] private Material _cursorNo;


        [SerializeField] private TurretAgent _turretPrefab;

        private GameObject _cursor;
        private Camera _camera;

        private Vector3 _offset;

        private Grid _grid;
        public Grid Grid => _grid;
        [SerializeField] private Vector2Int _targetPosition;
        [SerializeField] private GameObject _targetObject;
        [SerializeField] private Vector2Int _spawnerPosition;
        [SerializeField] private EnemySpawner _spawnerObject;

        private readonly Vector3 _iVector = Vector3.right;
        private readonly Vector3 _jVector = Vector3.forward;


        private void Awake()
        {
            ResizePlane();

            _camera = Camera.main;
            _cursor = Instantiate(_cursorPrefab, Vector3.zero, Quaternion.identity);
            _cursor.transform.localScale = _nodeSize * (Vector3.forward + Vector3.right) + 0.1f * Vector3.up;

            _grid = new Grid(_gridWidth, _gridHeight, _targetPosition, _spawnerPosition);

            for (int i = 0; i < _gridWidth; i++)
            {
                for (int j = 0; j < _gridHeight; j++)
                {
                    _grid[i, j].Position = RealPosition(i, j);
                }
            }
            PositionMarkers();
            _grid.UpdatePaths();
        }
        
        public void Raycast()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform != transform) // temp solution?
                {
                    _cursor.SetActive(false); //disable if ray did not reach the plane
                    return;
                }

                _cursor.SetActive(true);
                Vector3 difference = hit.point - _offset;
                Vector2Int pos = new Vector2Int((int) (difference.x / _nodeSize), (int) (difference.z / _nodeSize));
                _cursor.transform.position = _grid[pos].Position;

                Node node = _grid[pos];
                bool canBeOccupied = _grid.CanBeOccupied(pos);
                if (canBeOccupied)
                {
                    _cursor.GetComponent<MeshRenderer>().material = _cursorOk;
                }
                else
                {
                    _cursor.GetComponent<MeshRenderer>().material = _cursorNo;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (!node.Occupied)
                    {
                        if (canBeOccupied) 
                        {
                            node.Occupy(Instantiate(_turretPrefab, node.Position, Quaternion.identity).gameObject);
                            _grid.UpdatePaths();
                        }
                    }
                    else
                    {
                        node.Release();
                        _grid.UpdatePaths();
                    }

                }
            }
            else
            {
                _cursor.SetActive(false); //disable if cursor is not on the plane
            }
        }

        private Vector3 RealPosition(int i, int j)
        {
            return _offset + _nodeSize * (_iVector * i + _jVector * j + 0.5f * (_iVector + _jVector));
        }

        private void OnDrawGizmos()
        {
            DrawGizmosGrid();
            if (Application.isPlaying)
            {
                DrawGizmosArrows();
            }
        }

        private void DrawGizmosGrid()
        {
            Gizmos.color = Color.black;
            // Grid drawing
            for (int i = 1; i < _gridWidth; i++)
            {
                Gizmos.DrawLine(_offset + i * _iVector * _nodeSize,
                    _offset + i * _iVector * _nodeSize + _gridHeight * _jVector * _nodeSize);
            }

            for (int j = 1; j < _gridHeight; j++)
            {
                Gizmos.DrawLine(_offset + j * _jVector * _nodeSize,
                    _offset + j * _jVector * _nodeSize + _gridWidth * _iVector * _nodeSize);
            }

            // Origin drawing
            Gizmos.DrawSphere(_offset, 0.1f);
        }

        private void DrawGizmosArrows()
        {
            foreach (Node node in _grid.EnumerateAllNodes())
            {
                Vector3 start = node.Position;
                if (node.NextNode == null)
                {
                    continue;
                }

                Vector3 end = node.NextNode.Position;

                Vector3 dir = end - start;

                start -= dir * 0.25f;
                end -= dir * 0.75f;
                Gizmos.color = node.Availability switch
                {
                    OccupationAvailability.CanOccupy => Color.green,
                    OccupationAvailability.CanNotOccupy => Color.red,
                    OccupationAvailability.Undefined => Color.blue,
                    _ => Gizmos.color
                };
                Gizmos.DrawLine(start, end);
                Gizmos.DrawSphere(end, 0.1f);
            }
        }

        private void OnValidate()
        {
            ResizePlane();
            PositionMarkers();
        }


        private void ResizePlane()
        {
            float width = _gridWidth * _nodeSize;
            float height = _gridHeight * _nodeSize;

            // Default plane size is 10 by 10 (bodge)
            var transformThis = transform;
            transformThis.localScale = new Vector3(width * 0.1f, 1f, height * 0.1f);
            _offset = transformThis.position - 0.5f * new Vector3(width, 0f, height);
        }

        private void PositionMarkers()
        {
            _targetObject.transform.position = RealPosition(_targetPosition.x, _targetPosition.y);
            _spawnerObject.transform.position = RealPosition(_spawnerPosition.x, _spawnerPosition.y);
        }
    }
}