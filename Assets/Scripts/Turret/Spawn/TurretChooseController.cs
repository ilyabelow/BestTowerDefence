using Runtime;
using UnityEngine;

namespace Turret.Spawn
{
    public class TurretChooseController : IController
    {

        private TurretMarket _market;

        public TurretChooseController(TurretMarket market)
        {
            _market = market;
        }
        
        public void Tick()
        {
            float delta = Input.mouseScrollDelta.y;
            if (delta == 0) return;
            if (delta < 0) Game.Player.TurretMarket.ChooserBackward();
            if (delta > 0) Game.Player.TurretMarket.ChooserForward();
        }

        public void OnStart()
        {
        }

        public void OnStop()
        {
        }
    }
}