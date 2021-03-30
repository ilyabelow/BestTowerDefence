using Turret.Weapon;

namespace Turret
{
    public class TurretData
    {
        private TurretView _view;
        public TurretView View => _view;

        private IWeapon _weapon;
        public IWeapon Weapon => _weapon;

        private TurretAsset _asset;

        public TurretData(TurretAsset asset)
        {
            _asset = asset;
        }

        public void AttachView(TurretView view)
        {
            _view = view;
            _weapon = _asset.WeaponAsset.GetWeapon(View);
        }
    }
}