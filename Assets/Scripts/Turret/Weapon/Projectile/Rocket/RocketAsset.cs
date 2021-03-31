using Enemy;
using UnityEditor;
using UnityEngine;

namespace Turret.Weapon.Projectile.Rocket
{
    [CreateAssetMenu(fileName = "Rocket", menuName = "Assets/Weapons/Rocket")]
    public class RocketAsset : ProjectileAsset
    {
        public Rocket RocketPrefab;
        
        public float Speed;
        public float Damage;
        public float HitRadius;
        public float LifeTime;
        
        public override IProjectile CreateProjectile(Vector3 origin, Vector3 dir, EnemyData target)
        {
            var rocket = Instantiate(RocketPrefab, origin, Quaternion.LookRotation(dir));
            rocket.AttachAsset(this);
            rocket.AttachTarget(target);
            return rocket;
        }
    }
}