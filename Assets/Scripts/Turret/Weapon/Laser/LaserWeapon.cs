using System.Collections.Generic;
using Field;
using UnityEngine;

namespace Turret.Weapon.Laser
{
    public class LaserWeapon : IWeapon
    {
        private readonly LaserWeaponAsset _asset;
        private readonly TurretView _view;
        private readonly List<Node> _closeNodes;
        private readonly LineRenderer _beam;

        public LaserWeapon(LaserWeaponAsset asset, TurretView view)
        {
            _asset = asset;
            _view = view;
            _closeNodes = EnemySearch.GetCloseNodes(view.transform.position, asset.MaxDistance);
            _beam = Object.Instantiate(_asset.Beam);
        }
        
        public void TickWeapon()
        {
            var closestEnemy = EnemySearch.GetClosestEnemy(_view.transform.position, _closeNodes);
            if (closestEnemy == null)
            {
                _beam.gameObject.SetActive(false);
                return;
            }
            _beam.gameObject.SetActive(true);
            _view.Tower.LookAt(closestEnemy.View.transform);
            Vector3[] points = {_view.ProjectileOrigin.position, closestEnemy.View.AimTo.position};
            _beam.SetPositions(points);
            closestEnemy.ApplyDamage(_asset.DPS*Time.deltaTime);
        }

        public void Clean()
        {
            Object.Destroy(_beam.gameObject);
        }
    }
}