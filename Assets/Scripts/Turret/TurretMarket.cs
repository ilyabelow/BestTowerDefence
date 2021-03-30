using Runtime;

namespace Turret
{
    public class TurretMarket
    {
        private TurretAsset[] _turrets;

        // I'll fix this later
        public TurretAsset ChosenTurret => Game.CurrentLevel.TurretMarket[0];


        public TurretMarket(TurretAsset[] turrets)
        {
            _turrets = turrets;
        }
    }
}