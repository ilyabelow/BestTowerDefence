using System;
using Enemy;
using UnityEngine;
using Utils;

namespace Turret.Weapon.Projectile.Bullet
{
    public class Bullet : PooledMonoBehaviour, IProjectile
    {
        private bool _didHit;
        private bool _hitHandled;
        private EnemyData _hitEnemy;
        private BulletAsset _asset;
        private float _timeLeft;

        public override void AwakePooled()
        {
            _didHit = false;
            _hitHandled = false;
        }

        public void TickMovement()
        {
            transform.Translate(transform.forward * (_asset.Speed * Time.deltaTime), Space.World);
            // Missed bullets will eventually dissapear
            _timeLeft -= Time.deltaTime;
            if (_timeLeft < 0)
            {
                _didHit = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                return;
            }
            _didHit = true;

            if (other.CompareTag("Enemy"))
            {
                EnemyView enemyView = other.GetComponent<EnemyView>();
                if (enemyView != null)
                {
                    _hitEnemy = enemyView.Data;
                }
            }
        }

        public bool DidHit()
        {
            return _didHit;
        }

        public void AttachAsset(BulletAsset asset)
        {
            _asset = asset;
            _timeLeft = _asset.LifeTime;
        }

        public void HandleHit()
        {
            if (_hitHandled) return;
            _hitHandled = true;
            if (_hitEnemy != null && _hitEnemy.IsAlive)
            {
                _hitEnemy.ApplyDamage(_asset.Damage);
            }
            ObjectPool.DestroyPooled(this);
        }
    }
}