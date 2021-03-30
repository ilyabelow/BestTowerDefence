using System.Collections.Generic;
using Enemy;
using Turret;
using UnityEngine;

namespace Field
{
    public enum OccupationAvailability
    {
        CanOccupy,
        CanNotOccupy,
        Undefined
    }

    public class Node
    {
        public Vector3 Position { get; set; }

        private TurretData _occupant;
        public bool Occupied => _occupant != null;

        public Node NextNode { get; set; }
        public float PathWeight { get; set; }
        public bool Visited { get; set; }
        public OccupationAvailability Availability { get; set; }

        private readonly List<EnemyData> _enemyDatas = new List<EnemyData>();

        public IReadOnlyList<EnemyData> EnemyDatas => _enemyDatas;

        public void Occupy(TurretData occupant)
        {
            _occupant = occupant;
        }

        public TurretData Release()
        {
            var ret = _occupant;
            _occupant = null;
            return ret;
        }

        public void Reset()
        {
            PathWeight = float.MaxValue;
            Availability = OccupationAvailability.CanOccupy;
        }

        public void EnemyEntered(EnemyData data)
        {
            _enemyDatas.Add(data);
        }

        public void EnemyLeft(EnemyData data)
        {
            _enemyDatas.Remove(data);
        }

        public void CleanDead()
        {
            _enemyDatas.RemoveAll(enemy => !enemy.IsAlive);
        }
    }
}