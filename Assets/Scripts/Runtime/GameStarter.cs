using Assets;
using UnityEngine;

namespace Runtime
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private AssetRoot _assetRoot;

        void Start()
        {
            Game.SetAssetRoot(_assetRoot);
            Game.StartLevel(0);
        }
    }
}