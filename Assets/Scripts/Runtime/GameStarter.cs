using System.Resources;
using Assets;
using UnityEngine;

namespace Runtime
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private AssetRoot _assetRoot;

        void Start()
        {
            if (Game.Started)
            {
                Destroy(gameObject);
                return;
            }
            Game.SetAssetRoot(_assetRoot);
            Game.StartLevel(0);
        }
    }
}