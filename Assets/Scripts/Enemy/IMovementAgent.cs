namespace Enemy
{
    public interface IMovementAgent
    {
        void TickMovement();

        bool ShouldDie();
    }
}