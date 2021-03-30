using Runtime;

namespace Field
{
    public class GridRaycastController : IController
    {
        private readonly GridHolder _gridHolder;

        public GridRaycastController(GridHolder gridHolder)
        {
            _gridHolder = gridHolder;
        }

        public void Tick()
        {
            _gridHolder.Raycast();
        }

        public void OnStart()
        {
        }

        public void OnStop()
        {
        }
    }
}