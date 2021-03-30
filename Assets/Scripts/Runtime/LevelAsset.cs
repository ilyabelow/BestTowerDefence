using Enemy;
using Turret;
using UnityEditor;
using UnityEngine;

namespace Runtime
{
    [CreateAssetMenu(fileName = "Level", menuName = "Assets/LevelAsset")]
    public class LevelAsset : ScriptableObject
    {
        public SceneAsset SceneAsset;
        public SpawnWave[] SpawnWaves;
        public TurretAsset[] TurretMarket;
    }

    // Moved waves in one level asset because it's more convenient
    [System.Serializable]
    public class SpawnWave
    {
        public EnemyAsset EnemyAsset;
        public int Count;
        public float TimeBetweenSpawns;

        public float TimeBeforeStartWave;
    }
    
    
}