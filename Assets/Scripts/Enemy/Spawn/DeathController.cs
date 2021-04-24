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
                    enemyData.View.MovementAgent.Die();
                }
            }

            foreach (var enemyData in toDie)
            {
                Game.Player.EnemyDied(enemyData);
                Object.Destroy(enemyData.View.gameObject, 5f); //buffer for animations to play 
            }
            // //Debug
            // int saved = 0;
            // foreach (var node in Game.Player.Grid.EnumerateAllNodes())
            // {
            //     saved += node.EnemyDatas.Count;
            // }
            //
            // Debug.Log(saved);
        }

        public void OnStart()
        {
        }

        public void OnStop()
        {
        }
    }
}