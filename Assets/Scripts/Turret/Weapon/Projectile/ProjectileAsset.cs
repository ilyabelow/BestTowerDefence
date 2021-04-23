using Enemy;
using Runtime;
using UnityEngine;

namespace Turret.Weapon.Projectile
{
    public abstract class ProjectileAsset : ScriptableObject
    {
        protected Transform Folder
        {
            get
            {
                return Game.Player.BulletsFolder;
            }
        }
        public abstract IProjectile CreateProjectile(Vector3 origin, Vector3 dir, EnemyData target);
    }
}