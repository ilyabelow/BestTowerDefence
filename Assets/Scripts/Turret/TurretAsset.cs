using Turret.Weapon;
using UnityEngine;

namespace Turret
{
    [CreateAssetMenu(fileName = "Turret", menuName = "Assets/Turret Asset", order = 3)]
    public class TurretAsset : ScriptableObject
    {
        public TurretView TurretPrefab;
        public WeaponAsset WeaponAsset;
    }
}