using UnityEditor;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "Level", menuName = "Assets/LevelAsset", order = 1)]
    public class LevelAsset : ScriptableObject
    {
        public SceneAsset SceneAsset;
        public SpawnWave[] SpawnWaves;
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