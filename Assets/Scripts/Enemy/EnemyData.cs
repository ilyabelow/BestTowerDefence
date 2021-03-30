using Assets;
using Runtime;
using UnityEngine;

namespace Enemy
{
    // TODO events??
    public class EnemyData
    {
        private EnemyView _view;
        public EnemyView View => _view;

        private float _health;

        public bool IsAlive = true;

        public EnemyData(EnemyAsset asset)
        {
            _health = asset.StartHealth;
        }

        public void AttachView(EnemyView view)
        {
            _view = view;
        }

        public void ApplyDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
}