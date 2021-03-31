using Runtime;

namespace Enemy.Movement
{
    public class MovementController : IController
    {
        public void Tick()
        {
            foreach (var enemyData in Game.Player.EnemyDatas)
            {
                enemyData.View.MovementAgent.TickMovement();
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