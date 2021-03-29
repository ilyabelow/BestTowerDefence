using Runtime;

namespace Enemy
{
    public class MovementController : IController
    {
        public void Tick()
        {
            foreach (var enemyData in Game.Player.EnemyDatas)
            {
                var agent = enemyData.View.MovementAgent;
                agent.TickMovement();
                if (agent.ReachedGoal())
                {
                    enemyData.IsAlive = false;
                }
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