using System.Collections.Generic;
using Enemy;
using Field;
using Turret;
using Turret.Spawn;
using UnityEngine;
using Grid = Field.Grid;

namespace Runtime
{
    public class Player
    {
        private List<EnemyData> _enemyDatas = new List<EnemyData>();
        public IReadOnlyList<EnemyData> EnemyDatas => _enemyDatas;

        private List<TurretData> _turretDatas = new List<TurretData>();
        public IReadOnlyList<TurretData> TurretDatas => _turretDatas;
        
        public readonly GridHolder GridHolder;
        public readonly Grid Grid;
        public readonly TurretMarket TurretMarket;


        public Player()
        {
            GridHolder = Object.FindObjectOfType<GridHolder>();
            Grid = GridHolder.Grid;
            TurretMarket = new TurretMarket(Game.CurrentLevel.TurretMarket);
        }

        public void EnemySpawned(EnemyData data)
        {
            _enemyDatas.Add(data);
        }

        public void EnemyDied(EnemyData data)
        {
            _enemyDatas.Remove(data);
        }


        public void TurretPlaced(TurretData data)
        {
            _turretDatas.Add(data);
        }
        
        public void TurretRemoved(TurretData data)
        {
            _turretDatas.Remove(data);
        }
    }
}