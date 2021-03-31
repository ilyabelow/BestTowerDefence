using Field;
using Runtime;
using UnityEngine;
using Grid = Field.Grid;

namespace Turret.Spawn
{
    public class TurretSpawnController : IController
    {
        private readonly Grid _grid;
        private readonly TurretMarket _turretMarket;

        public TurretSpawnController(Grid grid, TurretMarket turretMarket)
        {
            _grid = grid;
            _turretMarket = turretMarket;
        }

        public void Tick()
        {
            if (!_grid.HasSelectedNode) return;
            if (!Input.GetMouseButtonDown(0)) return;
            
            Node node = _grid.SelectedNode;
            if (!node.Occupied)
            {
                if (_grid.CanBeOccupied(_grid.SelectedCoord))
                {
                    SpawnTurret(_turretMarket.ChosenTurret, node);
                    _grid.UpdatePaths();
                }
            }
            else
            {
                RemoveTurret(node);
                _grid.UpdatePaths();
            }
        }

        private void SpawnTurret(TurretAsset asset, Node node)
        {
            TurretData data = new TurretData(asset);
            TurretView view = Object.Instantiate(asset.TurretPrefab, node.Position, Quaternion.identity);
            view.AttachData(data);
            data.AttachView(view);

            node.Occupy(data);
            Game.Player.TurretPlaced(data);
        }

        private void RemoveTurret(Node node)
        {
            var data = node.Release();
            data.Weapon.Clean();
            Object.Destroy(data.View.gameObject);
            Game.Player.TurretRemoved(data);
        }

        public void OnStart()
        {
        }

        public void OnStop()
        {
        }
    }
}