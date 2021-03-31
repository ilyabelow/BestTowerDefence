using UnityEngine;

namespace Turret.Weapon.Field
{
    [CreateAssetMenu(fileName = "FieldWeapon", menuName = "Assets/Weapons/FieldWeapon")]
    public class FieldWeaponAsset : WeaponAsset
    {
        public GameObject FieldPrefab;
        public float DPS;
        public float Radius;
        
        public override IWeapon GetWeapon(TurretView view)
        {
            return new FieldWeapon(this, view);
        }
    }
}