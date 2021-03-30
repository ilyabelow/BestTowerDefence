using Runtime;

namespace Turret
{
    public class TurretShootController : IController
    {
        public void Tick()
        {
            foreach (var turretData in Game.Player.TurretDatas)
            {
                turretData.Weapon.TickWeapon();
            }
        }

        public void OnStart()
        {
        }

        public void OnStop()
        {
        }
    }
}