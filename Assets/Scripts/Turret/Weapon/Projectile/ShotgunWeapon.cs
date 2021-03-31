using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile
{
    public class ShotgunWeapon : ProjectileWeapon
    {
        private readonly ShotgunWeaponAsset _shotgunAsset;
        
        public ShotgunWeapon(ShotgunWeaponAsset asset, TurretView view) : base(asset, view)
        {
            _shotgunAsset = asset;
        }

        protected override void Shoot(EnemyData who)
        {
            for (int i = 0; i < _shotgunAsset.BulletCount; i++)
            {
                var direction = _view.ProjectileOrigin.forward;
                var deviation = Quaternion.AngleAxis(Random.value*360,direction) * _view.ProjectileOrigin.right * (Random.value*0.3f);

                _projectiles.Add(_asset.ProjectileAsset.CreateProjectile(_view.ProjectileOrigin.position, direction+deviation, who));
            }
        }
    }
}