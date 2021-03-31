using Turret.Weapon;
using UnityEngine;

namespace Turret
{
    [CreateAssetMenu(fileName = "Turret", menuName = "Assets/Turret Asset")]
    public class TurretAsset : ScriptableObject
    {
        public TurretView TurretPrefab;
        public WeaponAsset WeaponAsset;
    }
}