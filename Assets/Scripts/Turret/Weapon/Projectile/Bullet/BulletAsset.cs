using Enemy;
using Runtime;
using UnityEngine;
using Utils;

namespace Turret.Weapon.Projectile.Bullet
{   
    [CreateAssetMenu(fileName = "Bullet", menuName = "Assets/Weapons/Bullet")]
    public class BulletAsset : ProjectileAsset
    {

        public Bullet BulletPrefab;
        public float Speed;
        public float Damage;
        public float LifeTime;
        public override IProjectile CreateProjectile(Vector3 origin, Vector3 dir, EnemyData target = null)
        {
            var bullet = ObjectPool.InstantiatePooled(BulletPrefab, origin, Quaternion.LookRotation(dir), Folder);
            bullet.AttachAsset(this);
            return bullet;
        }
    }
}