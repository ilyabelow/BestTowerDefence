namespace Turret
{
    public class TurretMarket
    {
        private readonly TurretAsset[] _turrets;

        // I'll fix this later
        public TurretAsset ChosenTurret => _turrets[0];


        public TurretMarket(TurretAsset[] turrets)
        {
            _turrets = turrets;
        }
    }
}