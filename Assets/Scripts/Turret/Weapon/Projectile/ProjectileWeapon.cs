using System.Collections.Generic;
using Enemy;
using Field;
using Runtime;
using UnityEngine;
using Random = System.Random;

namespace Turret.Weapon.Projectile
{
    public class ProjectileWeapon : IWeapon
    {
        private List<IProjectile> _projectiles = new List<IProjectile>();

        private readonly ProjectileWeaponAsset _asset;

        private readonly TurretView _view;

        private float _countdown;

        private int _idleRotationDirection;

        private EnemyData _closestEnemy;

        private readonly List<Node> _closeNodes;

        public ProjectileWeapon(ProjectileWeaponAsset asset, TurretView view)
        {
            _asset = asset;
            _view = view;
            _countdown = _asset.RechargeTime;
            _closeNodes = Game.Player.Grid.GetNodesInCircle(view.transform.position, _asset.MaxDistance);

            // Beautification
            _idleRotationDirection = ((int) (UnityEngine.Random.value * 2)) * 2 - 1;
            _view.Tower.Rotate(Vector3.up, UnityEngine.Random.value * 360);
            _closestEnemy = EnemySearch.GetClosestEnemyAnywhere(_view.transform.position, _asset.MaxDistance);
        }

        public void TickWeapon()
        {
            TickFollow();
            TickShooting();
            TickProjectiles();
        }

        private void TickFollow()
        {
            if (_closestEnemy != null)
            {
                if (!_closestEnemy.IsAlive ||
                    !EnemySearch.CheckReach(_view.transform.position, _closestEnemy, _asset.MaxDistance))
                {
                    _closestEnemy = null;
                }
                else
                {
                    _view.Tower.LookAt(_closestEnemy.View.transform.position);
                }
            }
            else
            {
                _view.Tower.Rotate(Vector3.up, _asset.IdleRotationSpeed * _idleRotationDirection);
            }
        }

        private void TickShooting()
        {
            if (_countdown > Time.deltaTime)
            {
                _countdown -= Time.deltaTime;
                return;
            }

            _countdown = 0;
            _closestEnemy = EnemySearch.GetClosestEnemy(_view.transform.position, _asset.MaxDistance, _closeNodes);


            if (_closestEnemy != null)
            {
                TickFollow();
                Shoot();
                _countdown = _asset.RechargeTime;
                _idleRotationDirection = -_idleRotationDirection;
            }
        }

        private void TickProjectiles()
        {
            for (var index = 0; index < _projectiles.Count; index++)
            {
                var projectile = _projectiles[index];
                projectile.TickMovement();
                if (projectile.DidHit())
                {
                    _projectiles[index] = null;
                    projectile.HandleHit();
                }
            }

            _projectiles.RemoveAll(projectile => projectile == null);
        }

        private void Shoot()
        {
            _projectiles.Add(_asset.ProjectileAsset.CreateProjectile(_view.ProjectileOrigin.position,
                _view.ProjectileOrigin.forward));
        }
    }
}