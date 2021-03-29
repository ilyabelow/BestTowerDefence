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
        private readonly Grid _grid;
        private readonly Vector2Int _target;
        private readonly Vector2Int _start;

        public GridPathfinder(Grid grid, Vector2Int target, Vector2Int start)
        {
            _grid = grid;
            _target = target;
            _start = start;
        }

        public void UpdateField()
        {
            // Update next nodes

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

        public bool CanBeOccupied(Vector2Int nodeToTest)
        {
            Node node = _grid[nodeToTest];
            if (node.Availability == OccupationAvailability.CanOccupy) return true;
            if (node.Availability == OccupationAvailability.CanNotOccupy) return false;

            bool testResult = TestNode(nodeToTest);
            node.Availability = testResult ? OccupationAvailability.CanOccupy : OccupationAvailability.CanNotOccupy;
            return testResult;
        }

        private bool TestNode(Vector2Int nodeToTest)
        {
            foreach (Node node in _grid.EnumerateAllNodes())
            {
                node.Visited = false;
            }

            _grid[nodeToTest].Visited = true; //!!

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

        private IEnumerable<Connection> GetNeighbours(Vector2Int coordinate)
        {
            Vector2Int rightCoordinate = coordinate + Vector2Int.right;
            Vector2Int leftCoordinate = coordinate + Vector2Int.left;
            Vector2Int upCoordinate = coordinate + Vector2Int.up;
            Vector2Int downCoordinate = coordinate + Vector2Int.down;

            Vector2Int rightDownCoordinate = coordinate + Vector2Int.right + Vector2Int.down;
            Vector2Int leftUpCoordinate = coordinate + Vector2Int.left + Vector2Int.up;
            Vector2Int upRightCoordinate = coordinate + Vector2Int.up + Vector2Int.right;
            Vector2Int downLeftCoordinate = coordinate + Vector2Int.down + Vector2Int.left;

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
                yield return new Connection(rightDownCoordinate, (float) Math.Sqrt(2));
            }

            if (hasLeftUpNode)
            {
                yield return new Connection(leftUpCoordinate, (float) Math.Sqrt(2));
            }

            if (hasUpRightNode)
            {
                yield return new Connection(upRightCoordinate, (float) Math.Sqrt(2));
            }

            if (hasDownLeftNode)
            {
                yield return new Connection(downLeftCoordinate, (float) Math.Sqrt(2));
            }
        }
    }
}