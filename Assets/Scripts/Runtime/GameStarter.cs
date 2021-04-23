using System.Resources;
using Assets;
using UnityEngine;

namespace Runtime
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private AssetRoot _assetRoot;
        [SerializeField] private int _levelToStart;

        void Start()
        {
            if (Game.Started)
            {
                Destroy(gameObject);
                return;
            }
            Game.SetAssetRoot(_assetRoot);
            Game.StartLevel(_levelToStart);
        }
    }
}