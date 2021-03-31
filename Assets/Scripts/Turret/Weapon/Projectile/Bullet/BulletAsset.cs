using Enemy;
using UnityEngine;

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
            var bullet = Instantiate(BulletPrefab, origin, Quaternion.LookRotation(dir));
            bullet.AttachAsset(this);
            return bullet;
        }
    }
}