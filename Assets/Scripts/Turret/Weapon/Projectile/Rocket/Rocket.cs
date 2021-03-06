using System;
using Enemy;
using UnityEngine;
using Utils;

namespace Turret.Weapon.Projectile.Rocket
{
    public class Rocket : PooledMonoBehaviour, IProjectile
    {
        private bool _didHit;
        private bool _hitHandled;
        private EnemyData _target;
        private RocketAsset _asset;
        private Vector3 _dir;
        private float _timeLeft;

        public override void AwakePooled()
        {
            _didHit = false;
            _hitHandled = false;
            _target = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            _didHit = true;
        }
        
        public void TickMovement()
        {
            if (_target.IsAlive)
            {
                var enemyPosition = _target.View.AimTo.position;
                _dir = (enemyPosition - transform.position).normalized;
                transform.LookAt(enemyPosition);
            }
            transform.position +=_asset.Speed * Time.deltaTime * _dir;
            _timeLeft -= Time.deltaTime;
            if (_timeLeft < 0)
            {
                _didHit = true;
            }
        }

        public bool DidHit()
        {
            return _didHit;
        }

        public void HandleHit()
        {
            if (_hitHandled) return;
            _hitHandled = true;
            var toDamage = EnemySearch.GetEnemiesInRadius(transform.position, _asset.HitRadius);
            foreach (var enemyData in toDamage)
            {
                enemyData.ApplyDamage(_asset.Damage);
            }
            ObjectPool.DestroyPooled(this);
        }

        public void AttachAsset(RocketAsset asset)
        {
            _asset = asset;
            _timeLeft = _asset.LifeTime;
        }
        
        public void AttachTarget(EnemyData target)
        {
            _target = target;
        }
    }
}