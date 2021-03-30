using System.Collections.Generic;
using Runtime;
using UnityEngine;

namespace Enemy
{
    public class DeathController : IController
    {
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
            foreach (var node in Game.Player.Grid.EnumerateAllNodes())
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