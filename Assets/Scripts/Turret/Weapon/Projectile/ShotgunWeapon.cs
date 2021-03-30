using UnityEngine;

namespace Turret.Weapon.Projectile
{
    public class ShotgunWeapon : ProjectileWeapon
    {
        public ShotgunWeapon(ShotgunWeaponAsset asset, TurretView view) : base(asset, view)
        {
        }

        protected override void Shoot()
        {
            for (int i = 0; i <= 5; i++)
            {
                var direction = _view.ProjectileOrigin.forward;
                var deviation = Quaternion.AngleAxis(Random.value*360,direction) * _view.ProjectileOrigin.right * (Random.value*0.3f);

                _projectiles.Add(_asset.ProjectileAsset.CreateProjectile(_view.ProjectileOrigin.position , direction+deviation));
            }
        }
    }
}