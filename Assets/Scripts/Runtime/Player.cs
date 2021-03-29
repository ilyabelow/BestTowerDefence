using System.Collections.Generic;
using Enemy;
using Field;
using UnityEngine;
using Grid = Field.Grid;

namespace Runtime
{
    public class Player
    {
        private List<EnemyData> _enemyDatas = new List<EnemyData>();

        public readonly MovementCursor MovementCursor;

        public IReadOnlyList<EnemyData> EnemyDatas => _enemyDatas;

        public Player()
        {
            MovementCursor = GameObject.FindObjectOfType<MovementCursor>();
        }

        public void EnemySpawned(EnemyData data)
        {
            _enemyDatas.Add(data);
        }
        
        public void EnemyDied(EnemyData data)
        {
            _enemyDatas.Remove(data);
        }
    }
}