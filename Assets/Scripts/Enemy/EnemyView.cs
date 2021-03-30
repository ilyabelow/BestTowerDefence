using System;
using Enemy.MovementAgent;
using UnityEngine;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyData _enemyData;
        public EnemyData Data => _enemyData;

        private IMovementAgent _movementAgent;
        public IMovementAgent MovementAgent => _movementAgent;

        public void AttachData(EnemyData data)
        {
            _enemyData = data;
        }

        public void AttachMovementAgent(IMovementAgent agent)
        {
            _movementAgent = agent;
        }
    }
}