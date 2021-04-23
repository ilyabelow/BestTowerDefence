namespace Enemy.Movement
{
    public interface IMovementAgent
    {
        void TickMovement();

        void Die();
    }
}