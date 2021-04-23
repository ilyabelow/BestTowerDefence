using Runtime;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "AssetRoot", menuName = "Assets/AssetRoot")]
    public class AssetRoot : ScriptableObject
    {
        public LevelAsset[] Levels;
    }
}