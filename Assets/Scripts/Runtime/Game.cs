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
        public static LevelAsset CurrentLevel => _currentLevel;

        public static bool Started => _assetRoot != null;

        public static void SetAssetRoot(AssetRoot assetRoot)
        {
            _assetRoot = assetRoot;
        }

        public static void StartLevel(int index)
        {
            _currentLevel = _assetRoot.Levels[index];
            // In case we started the scene corresponding to level
            if (_currentLevel.SceneAsset.name.Equals(SceneManager.GetActiveScene().name))
            {
                StartRunner(null);
                return;
            }
            
            // Async because the scene is only loaded on the next frame
            AsyncOperation operation = SceneManager.LoadSceneAsync(_currentLevel.SceneAsset.name);
            operation.completed += StartRunner;
        }

        private static void StartRunner(AsyncOperation operation)
        {
            _player = new Player();
            var runner = new GameObject("Runner");
            runner.AddComponent<Runner>();
            _runner = runner.GetComponent<Runner>();
            _runner.StartRunning();
        }

        public static void StopPlayer()
        {
            _runner.StopRunning();
        }
    }
}