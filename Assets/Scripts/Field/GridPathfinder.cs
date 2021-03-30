using System;
using System.Collections.Generic;
using UnityEngine;


namespace Field
{
    
    public readonly struct Connection
    {
        public Connection(Vector2Int coordinate, float weight)
        {
            Coordinate = coordinate;
            Weight = weight;
        }

        public Vector2Int Coordinate { get; }
        public float Weight { get; }
    }

    public class GridPathfinder
    {
        private static readonly float Sqrt2 = (float) Math.Sqrt(2);
        
        private readonly Grid _grid;
        private readonly Vector2Int _start;
        private readonly Vector2Int _target;

        public GridPathfinder(Grid grid)
        {
            _grid = grid;
            _start = grid.StartCoord;
            _target = grid.TargetCoord;
        }

        public void UpdateField()
        {
            foreach (Node node in _grid.EnumerateAllNodes())
            {
                node.Reset();
            }

            Queue<Vector2Int> queue = new Queue<Vector2Int>();

            queue.Enqueue(_target);
            _grid[_target].PathWeight = 0f;
            _grid[_target].NextNode = null;

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                Node currentNode = _grid[current];

                foreach (Connection neighbour in GetNeighbours(current))
                {
                    float weightToTarget = currentNode.PathWeight + neighbour.Weight;
                    Node neighbourNode = _grid[neighbour.Coordinate];
                    if (weightToTarget >= neighbourNode.PathWeight) continue;

                    neighbourNode.NextNode = currentNode;
                    neighbourNode.PathWeight = weightToTarget;
                    queue.Enqueue(neighbour.Coordinate);
                }
            }

            // Updating availability on the shortest path
            Node curNode = _grid[_start];
            while (curNode != null)
            {
                curNode.Availability = OccupationAvailability.Undefined;
                curNode = curNode.NextNode;
            }

            _grid[_start].Availability = OccupationAvailability.CanNotOccupy;
            _grid[_target].Availability = OccupationAvailability.CanNotOccupy;
        }

        public bool CanBeOccupied(Vector2Int coord)
        {
            Node node = _grid[coord];
            if (node.Availability == OccupationAvailability.CanOccupy) return true;
            if (node.Availability == OccupationAvailability.CanNotOccupy) return false;

            bool testResult = TestNode(coord);
            node.Availability = testResult ? OccupationAvailability.CanOccupy : OccupationAvailability.CanNotOccupy;
            return testResult;
        }

        private bool TestNode(Vector2Int coord)
        {
            foreach (Node node in _grid.EnumerateAllNodes())
            {
                node.Visited = false;
            }

            _grid[coord].Visited = true; //!!

            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(_target);
            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                foreach (Connection neighbour in GetNeighbours(current))
                {
                    Node neighbourNode = _grid[neighbour.Coordinate];
                    if (neighbourNode.Visited) continue;
                    neighbourNode.Visited = true;
                    queue.Enqueue(neighbour.Coordinate);
                }
            }

            return _grid[_start].Visited;
        }

        private IEnumerable<Connection> GetNeighbours(Vector2Int coord)
        {
            Vector2Int rightCoordinate = coord + Vector2Int.right;
            Vector2Int leftCoordinate = coord + Vector2Int.left;
            Vector2Int upCoordinate = coord + Vector2Int.up;
            Vector2Int downCoordinate = coord + Vector2Int.down;

            Vector2Int rightDownCoordinate = coord + Vector2Int.right + Vector2Int.down;
            Vector2Int leftUpCoordinate = coord + Vector2Int.left + Vector2Int.up;
            Vector2Int upRightCoordinate = coord + Vector2Int.up + Vector2Int.right;
            Vector2Int downLeftCoordinate = coord + Vector2Int.down + Vector2Int.left;

            bool hasRightNode = rightCoordinate.x < _grid.Width && !_grid[rightCoordinate].Occupied;
            bool hasLeftNode = leftCoordinate.x >= 0 && !_grid[leftCoordinate].Occupied;
            bool hasUpNode = upCoordinate.y < _grid.Height && !_grid[upCoordinate].Occupied;
            bool hasDownNode = downCoordinate.y >= 0 && !_grid[downCoordinate].Occupied;

            bool hasRightDownNode = rightCoordinate.x < _grid.Width && downCoordinate.y >= 0 && // not edge
                                    !_grid[rightDownCoordinate].Occupied && // not occupied
                                    !_grid[rightCoordinate].Occupied && !_grid[downCoordinate].Occupied; // no cornering
            bool hasLeftUpNode = leftCoordinate.x >= 0 && upCoordinate.y < _grid.Height &&
                                 !_grid[leftUpCoordinate].Occupied &&
                                 !_grid[leftCoordinate].Occupied && !_grid[upCoordinate].Occupied;
            bool hasUpRightNode = upCoordinate.y < _grid.Height && rightCoordinate.x < _grid.Width &&
                                  !_grid[upRightCoordinate].Occupied &&
                                  !_grid[upCoordinate].Occupied && !_grid[rightCoordinate].Occupied;
            bool hasDownLeftNode = downCoordinate.y >= 0 && leftCoordinate.x >= 0 &&
                                   !_grid[downLeftCoordinate].Occupied &&
                                   !_grid[downCoordinate].Occupied && !_grid[leftCoordinate].Occupied;

            if (hasRightNode)
            {
                yield return new Connection(rightCoordinate, 1);
            }

            if (hasLeftNode)
            {
                yield return new Connection(leftCoordinate, 1);
            }

            if (hasUpNode)
            {
                yield return new Connection(upCoordinate, 1);
            }

            if (hasDownNode)
            {
                yield return new Connection(downCoordinate, 1);
            }

            if (hasRightDownNode)
            {
                yield return new Connection(rightDownCoordinate, Sqrt2);
            }

            if (hasLeftUpNode)
            {
                yield return new Connection(leftUpCoordinate, Sqrt2);
            }

            if (hasUpRightNode)
            {
                yield return new Connection(upRightCoordinate, Sqrt2);
            }

            if (hasDownLeftNode)
            {
                yield return new Connection(downLeftCoordinate, Sqrt2);
            }
        }
    }
}