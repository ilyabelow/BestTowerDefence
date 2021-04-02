using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile.Shotgun
{
    public class Shotgun : IProjectile
    {
        private readonly List<IProjectile> _projectiles = new List<IProjectile>();

        public Shotgun(Vector3 origin, Vector3 dir, EnemyData target, ShotgunAsset asset)
        {
            for (int i = 0; i < asset.Load; i++)
            {
                var perp = Vector3.Cross(dir, Vector3.one).normalized;
                var deviation = Quaternion.AngleAxis(Random.value * 360, dir) * perp * (Random.value * asset.MaxDispersion);
                _projectiles.Add(asset.InternalAsset.CreateProjectile(origin, dir + deviation, target));
            }
        }

        public void TickMovement()
        {
            for (var i = 0; i < _projectiles.Count; i++)
            {
                var projectile = _projectiles[i];
                projectile.TickMovement();
                if (projectile.DidHit())
                {
                    _projectiles[i] = null;
                    projectile.HandleHit();
                }
            }

            _projectiles.RemoveAll(projectile => projectile == null);
        }

        public bool DidHit()
        {
            bool didHit = false;
            foreach (var projectile in _projectiles)
            {
                didHit |= projectile.DidHit();
            }
            return didHit;
        }

        public void HandleHit()
        {
            foreach (var projectile in _projectiles)
            {
                projectile.HandleHit();
            }
        }
    }
}