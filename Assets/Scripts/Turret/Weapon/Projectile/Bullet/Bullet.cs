using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile.Bullet
{
    public class Bullet : MonoBehaviour, IProjectile
    {
        private bool _didHit;
        private EnemyData _hitEnemy;
        private BulletAsset _asset;

        public void TickMovement()
        {
            transform.Translate(transform.forward * (_asset.Speed * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
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
        }

        public void HandleHit()
        {
            _hitEnemy?.ApplyDamage(_asset.Damage);
            Destroy(gameObject);
        }
    }
}