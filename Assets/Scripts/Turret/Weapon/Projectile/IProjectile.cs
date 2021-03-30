namespace Turret.Weapon.Projectile
{
    public interface IProjectile
    {
        void TickMovement();
        bool DidHit();
        void HandleHit();
    }
}