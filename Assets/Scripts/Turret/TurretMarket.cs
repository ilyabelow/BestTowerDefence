namespace Turret
{
    public class TurretMarket
    {
        private readonly TurretAsset[] _turrets;

        private int _chosen = 0;

        // I'll fix this later
        public TurretAsset ChosenTurret => _turrets[_chosen];


        public TurretMarket(TurretAsset[] turrets)
        {
            _turrets = turrets;
        }

        public void ChooserForward()
        {
            _chosen += 1;
            _chosen %= _turrets.Length;
        }


        public void ChooserBackward()
        {
            _chosen -= 1;
            _chosen = _chosen < 0 ? _turrets.Length - 1 : _chosen;
        }
    }
}