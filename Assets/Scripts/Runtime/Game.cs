using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public static class Game
    {
        private static Player _player;
        private static Runner _runner;

        private static AssetRoot _assetRoot;
        private static LevelAsset _currentLevel;

        public static Player Player => _player;
        public static AssetRoot AssetRoot => _assetRoot;
        public static LevelAsset CurrentLevel => _currentLevel;

        public static void SetAssetRoot(AssetRoot assetRoot)
        {
            _assetRoot = assetRoot;
        }

        public static void StartLevel(int index)
        {
            _currentLevel = _assetRoot.Levels[index];
            // Async because the scene is only loaded on the next frame
            AsyncOperation operation = SceneManager.LoadSceneAsync(_currentLevel.SceneAsset.name);
            operation.completed += StartRunner;
        }

        private static void StartRunner(AsyncOperation operation)
        {
            _player = new Player();
            _runner = Object.Instantiate(_assetRoot.RunnerPrefab);
            _runner.StartRunning();
        }

        public static void StopPlayer()
        {
            _runner.StopRunning();
        }
    }
}