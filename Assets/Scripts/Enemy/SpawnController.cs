using Assets;
using Runtime;
using UnityEngine;

namespace Enemy
{
    public class SpawnController : IController
    {
        private float _spawnStartTime;
        private float _passedTimeAtPreviousFrame;

        private readonly SpawnWave[] _spawnWaves;


        public SpawnController(SpawnWave[] spawnWaves)
        {
            _spawnWaves = spawnWaves;
        }

        public void Tick()
        {
            // Just copied and pasted the algo TODO make up a better solution
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
            var view = Object.Instantiate(asset._viewPrefab); 
            data.AttachView(view);
            view.AttachData(data);
            
            // Very bad ((
            switch (asset._movementType)
            {
                case MovementType.Grid:
                    // temp solution TODO move to controller???
                    view.AttachMovementAgent(
                        new GridMovementAgent(Game.Player.MovementCursor.Grid, view.transform, asset._speed)
                    );
                    break;
                case MovementType.Flying:
                    Debug.Log("Not implemented yet");
                    break;
            }

            Game.Player.EnemySpawned(data);
        }
    }
}