using Runtime;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "AssetRoot", menuName = "Assets/AssetRoot", order = 0)]
    public class AssetRoot : ScriptableObject
    {
        public Runner RunnerPrefab;
        public LevelAsset[] Levels;
    }
}