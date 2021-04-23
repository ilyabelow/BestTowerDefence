using Enemy.Movement;
using Runtime;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy.Spawn
{
    public class SpawnController : IController
    {
        private readonly Grid _grid;
        private readonly SpawnWave[] _spawnWaves;
        
        private float _spawnStartTime;
        private float _passedTimeAtPreviousFrame;

        
        public SpawnController(Grid grid, SpawnWave[] spawnWaves)
        {
            _grid = grid;
            _spawnWaves = spawnWaves;
        }

        public void Tick()
        {
            float passedTime = Time.time - _spawnStartTime;
            float timeToSpawn = 0f;
            foreach (SpawnWave wave in _spawnWaves)
            {
                timeToSpawn += wave.TimeBeforeStartWave;

                for (int i = 0; i < wave.Count; i++)
                {
                    if (passedTime >= timeToSpawn && _passedTimeAtPreviousFrame < timeToSpawn)
                    {
                        SpawnEnemy(wave.EnemyAsset);
                    }

                    if (i < wave.Count - 1)
                    {
                        timeToSpawn += wave.TimeBetweenSpawns;
                    }
                }
            }

            _passedTimeAtPreviousFrame = passedTime;
        }

        public void OnStart()
        {
            _spawnStartTime = Time.time;
        }

        public void OnStop()
        {
        }

        private void SpawnEnemy(EnemyAsset asset)
        {
            EnemyData data = new EnemyData(asset);
            var view = Object.Instantiate(asset.EnemyPrefab, Game.Player.EnemiesFolder);
            data.AttachView(view);
            view.AttachData(data);

            // Very bad =(
            switch (asset.MovementType)
            {
                case MovementType.Grid:
                    view.AttachMovementAgent(
                        new GridMovementAgent(view, _grid, asset.Speed)
                    );
                    break;
                case MovementType.Flying:
                    view.AttachMovementAgent(
                        new AirMovementAgent(view, _grid, asset.Speed)
                    );
                    break;
            }

            Game.Player.EnemySpawned(data);
        }
    }
}