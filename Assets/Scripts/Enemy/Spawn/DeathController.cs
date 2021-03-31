using System.Collections.Generic;
using Runtime;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class DeathController : IController
    {
        private readonly Grid _grid;

        public DeathController(Grid grid)
        {
            _grid = grid;
        }
        
        public void Tick()
        {
            var toDie = new List<EnemyData>();
            foreach (var enemyData in Game.Player.EnemyDatas)
            {
                if (!enemyData.IsAlive)
                {
                    toDie.Add(enemyData);
                }
            }

            foreach (var enemyData in toDie)
            {
                Game.Player.EnemyDied(enemyData);
                Object.Destroy(enemyData.View.gameObject);
            }

            // Temporal solution!!!!!!!!!!!!!!!!!
            foreach (var node in _grid.EnumerateAllNodes())
            {
                node.CleanDead();
            }
        }

        public void OnStart()
        {
        }

        public void OnStop()
        {
        }
    }
}