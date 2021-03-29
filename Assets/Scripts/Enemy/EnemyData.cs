using Assets;
using Runtime;
using UnityEngine;

namespace Enemy
{
    // TODO events??
    public delegate void OnTargetReached();

    public class EnemyData
    {
        private EnemyView _view;
        public EnemyView View => _view;

        private float Health;

        public bool IsAlive = true;

        public EnemyData(EnemyAsset asset)
        {
            Health = asset._startHealth;
        }

        public void AttachView(EnemyView view)
        {
            _view = view;
        }
    }
}