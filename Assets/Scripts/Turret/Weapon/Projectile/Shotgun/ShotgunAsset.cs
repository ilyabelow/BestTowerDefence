using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile.Shotgun
{
    [CreateAssetMenu(fileName = "Shotgun", menuName = "Assets/Weapons/Shotgun")]
    public class ShotgunAsset : ProjectileAsset
    {
        public ProjectileAsset InternalAsset;
        public int Load;
        public float MaxDispersion;
        
        public override IProjectile CreateProjectile(Vector3 origin, Vector3 dir, EnemyData target)
        {
            return new Shotgun(origin, dir, target, this);
        }
    }
}