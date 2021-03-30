using UnityEngine;

namespace Turret.Weapon.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileWeapon", menuName = "Assets/Weapons/Projectile Weapon")]
    public class ProjectileWeaponAsset : WeaponAsset
    {
        public ProjectileAsset ProjectileAsset;
        public float RechargeTime;
        public float MaxDistance;
        public float IdleRotationSpeed;

        public override IWeapon GetWeapon(TurretView view)
        {
            return new ProjectileWeapon(this, view);
        }
    }
}