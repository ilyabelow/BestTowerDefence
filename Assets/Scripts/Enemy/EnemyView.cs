using System;
using Enemy.Movement;
using UnityEngine;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyData _enemyData;
        public EnemyData Data => _enemyData;

        private IMovementAgent _movementAgent;
        public IMovementAgent MovementAgent => _movementAgent;

        [SerializeField] private Transform _aimTo;
        public Transform AimTo => _aimTo;

        private Animator _animator;
        public Animator Animator => _animator;

        public void Awake()
        {
            _animator = GetComponent<Animator>();
        }

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