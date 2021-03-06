using System.Collections.Generic;
using Field;
using Runtime;
using UnityEngine;

namespace Turret.Weapon.Field
{
    public class FieldWeapon : IWeapon
    {
        private readonly FieldWeaponAsset _asset;

        private readonly GameObject _field;
        private readonly List<Node> _closeNodes;
        
        public FieldWeapon(FieldWeaponAsset asset, TurretView view)
        {
            _asset = asset;
            _field = Object.Instantiate(asset.FieldPrefab, view.ProjectileOrigin.position, Quaternion.identity, Game.Player.BulletsFolder);
            _field.transform.localScale = Vector3.one * asset.Radius;
            _closeNodes = EnemySearch.GetCloseNodes(view.transform.position, asset.Radius);
            view.Tower.Rotate(Vector3.up, UnityEngine.Random.value * 360);
        }
        
        public void TickWeapon()
        {
            foreach (var node in _closeNodes)
            {
                foreach (var enemy in node.EnemyDatas)
                {
                    enemy.ApplyDamage(_asset.DPS * Time.deltaTime);
                }
            }
        }

        public void Clean()
        {
            Object.Destroy(_field.gameObject);
        }
    }
}