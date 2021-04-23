using Enemy;
using Runtime;
using UnityEditor;
using UnityEngine;
using Utils;

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
            var rocket = ObjectPool.InstantiatePooled(RocketPrefab, origin, Quaternion.LookRotation(dir), Folder);
            rocket.AttachAsset(this);
            rocket.AttachTarget(target);
            return rocket;
        }
    }
}