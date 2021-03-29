using System.Collections.Generic;
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

        public uint Width => _width;

        public uint Height => _height;

        public Node this[int x, int y] => _nodes[x, y];
        public Node this[Vector2Int coord] => _nodes[coord.x, coord.y];

        private readonly GridPathfinder _gridPathfinder;

        public Node StartNode => this[_start];

        public Node TargetNode => this[_target];

        public Grid(uint width, uint height, Vector2Int target, Vector2Int start)
        {
            _width = width;
            _height = height;
            _target = target;
            _start = start;
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

    }
}