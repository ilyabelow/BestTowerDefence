using UnityEngine;

namespace Turret.Weapon
{
    public abstract class WeaponAsset : ScriptableObject
    {
        public abstract IWeapon GetWeapon(TurretView view);
    }
}