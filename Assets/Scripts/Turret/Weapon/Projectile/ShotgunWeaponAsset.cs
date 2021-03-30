using UnityEngine;

namespace Turret.Weapon.Projectile
{
    [CreateAssetMenu(fileName = "ShotgunWeapon", menuName = "Assets/Weapons/Shotgun Weapon")]
    public class ShotgunWeaponAsset : ProjectileWeaponAsset
    {
        // Redundant...
        public override IWeapon GetWeapon(TurretView view)
        {
            return new ShotgunWeapon(this, view);
        }

    }
}