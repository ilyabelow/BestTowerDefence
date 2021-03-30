using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Field
{
    public class Grid
    {
        private Node[,] _nodes;

        private readonly uint _width;
        private readonly uint _height;
        private readonly Vector2Int _target;
        private readonly Vector2Int _start;
        private readonly float _nodeSize;
        private readonly Vector3 _offset;

        public uint Width => _width;

        public uint Height => _height;

        public Node this[int x, int y] => _nodes[x, y];
        public Node this[Vector2Int coord] => _nodes[coord.x, coord.y];

        private readonly GridPathfinder _gridPathfinder;

        public Node StartNode => this[_start];
        public Vector2Int StartCoord => _start;
        public Node TargetNode => this[_target];
        public Vector2Int TargetCoord => _target;

        private Vector2Int _selectedCoord;

        public Vector2Int SelectedCoord => _selectedCoord;
        public Node SelectedNode => this[_selectedCoord];
        public bool HasSelectedNode { get; private set; }

        public Grid(uint width, uint height, Vector2Int target, Vector2Int start, float nodeSize, Vector3 offset)
        {
            _width = width;
            _height = height;
            _target = target;
            _start = start;
            _nodeSize = nodeSize;
            _offset = offset;
            _gridPathfinder = new GridPathfinder(this, target, start);
            _nodes = new Node[_width, _height];
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _nodes[i, j] = new Node();
                }
            }
        }

        public bool CanBeOccupied(Vector2Int node)
        {
            return _gridPathfinder.CanBeOccupied(node);
        }

        public IEnumerable<Node> EnumerateAllNodes()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    yield return _nodes[i, j];
                }
            }
        }

        public void UpdatePaths()
        {
            _gridPathfinder.UpdateField();
        }

        public void SelectNode(Vector2Int coord)
        {
            _selectedCoord = coord;
            HasSelectedNode = true;

        }
        
        public void UnselectNode()
        {
            HasSelectedNode = false;
        }

        
        public Node GetNodeAtPoint(Vector3 point)
        {
            Vector3 difference = point - _offset;
            int x = (int) (difference.x / _nodeSize);
            int y = (int) (difference.z / _nodeSize);
            return this[x, y];
        }
        
        
        public List<Node> GetNodesInCircle(Vector3 point, float radius)
        {
            var list = new List<Node>();
            foreach (var node in EnumerateAllNodes())
            {
                if ((node.Position - point).sqrMagnitude < radius * radius)
                {
                    list.Add(node);
                }
            }
            return list;
        }
    }
}