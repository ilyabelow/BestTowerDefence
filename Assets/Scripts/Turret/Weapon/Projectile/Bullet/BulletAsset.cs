using UnityEngine;

namespace Turret.Weapon.Projectile.Bullet
{   
    [CreateAssetMenu(fileName = "Bullet", menuName = "Assets/Weapons/Bullet")]
    public class BulletAsset : ProjectileAsset
    {

        public Bullet BulletPrefab;
        public float Speed;
        public float Damage;
        public override IProjectile CreateProjectile(Vector3 origin, Vector3 dir)
        {
            var bullet = Instantiate(BulletPrefab, origin, Quaternion.LookRotation(dir));
            bullet.AttachAsset(this);
            return bullet;
        }
    }
}