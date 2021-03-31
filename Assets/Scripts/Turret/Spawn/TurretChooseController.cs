using Runtime;
using UnityEngine;

namespace Turret.Spawn
{
    public class TurretChooseController : IController
    {
        private readonly TurretMarket _market;

        public TurretChooseController(TurretMarket market)
        {
            _market = market;
        }
        
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _market.ChooserForward();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _market.ChooserBackward();
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