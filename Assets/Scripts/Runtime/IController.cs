namespace Runtime
{
    public interface IController
    {
        void Tick();
        
        void OnStart();

        void OnStop();
    }
}