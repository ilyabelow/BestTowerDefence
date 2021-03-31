using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using Runtime;
using UnityEngine;

namespace Turret.Weapon
{
    public static class EnemySearch
    {
        [CanBeNull]
        public static EnemyData GetClosestEnemyAnywhere(Vector3 center, float maxDistance)
        {
            float maxSqrDistance = maxDistance * maxDistance;

            float minSqrDistance = float.MaxValue;
            EnemyData closestEnemy = null;

            foreach (EnemyData enemyData in Game.Player.EnemyDatas)
            {
                if (!enemyData.IsAlive) continue;
                float sqrDistance = (enemyData.View.transform.position - center).sqrMagnitude;
                if (sqrDistance > maxSqrDistance || sqrDistance >= minSqrDistance)
                {
                    continue;
                }

                minSqrDistance = sqrDistance;
                closestEnemy = enemyData;
            }

            return closestEnemy;
        }

        public static EnemyData GetClosestEnemy(Vector3 center, IEnumerable<Node> nodes)
        {
            float minSqrDistance = float.MaxValue;
            EnemyData closestEnemy = null;

            foreach (var node in nodes)
            {
                foreach (EnemyData enemyData in node.EnemyDatas)
                {
                    if (!enemyData.IsAlive) continue;
                    float sqrDistance = (enemyData.View.transform.position - center).sqrMagnitude;
                    if (sqrDistance >= minSqrDistance)
                    {
                        continue;
                    }

                    minSqrDistance = sqrDistance;
                    closestEnemy = enemyData;
                }
            }
            return closestEnemy;
        }

        public static List<EnemyData> GetEnemiesInRadius(Vector3 center, float radius)
        {
            var list = new List<EnemyData>();
            var radiusSqr = radius * radius;
            foreach (var enemyData in Game.Player.EnemyDatas)
            {
                if ((enemyData.View.transform.position - center).sqrMagnitude <= radiusSqr)
                {
                    list.Add(enemyData);
                }
            }

            return list;
        }

        public static bool CheckReach(Vector3 center, EnemyData enemy, float distance)
        {
            return (enemy.View.transform.position - center).sqrMagnitude < distance * distance;
        }
    }
}