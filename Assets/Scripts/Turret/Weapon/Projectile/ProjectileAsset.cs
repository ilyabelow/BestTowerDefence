using UnityEngine;

namespace Turret.Weapon.Projectile
{
    public abstract class ProjectileAsset : ScriptableObject
    {
        public abstract IProjectile CreateProjectile(Vector3 origin, Vector3 dir);
    }
}