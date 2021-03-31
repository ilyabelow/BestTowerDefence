using UnityEngine;

namespace Turret.Weapon.Laser
{        
    [CreateAssetMenu(fileName = "LaserWeapon", menuName = "Assets/Weapons/Laser Weapon")]
    public class LaserWeaponAsset : WeaponAsset
    {
        public LineRenderer Beam;
        public float DPS;
        public float MaxDistance;
        
        public override IWeapon GetWeapon(TurretView view)
        {
            return new LaserWeapon(this, view);
        }
    }
}