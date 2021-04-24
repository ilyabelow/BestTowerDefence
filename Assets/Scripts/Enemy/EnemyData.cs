using UnityEngine;

namespace Enemy
{
    public class EnemyData
    {
        private EnemyView _view;
        public EnemyView View => _view;

        private bool _isAlive = true;
        public bool IsAlive => _isAlive;
        
        private float _health;
        private static readonly int Died = Animator.StringToHash("Died");
        private static readonly int Won = Animator.StringToHash("Won");


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
                Die();
            }
        }

        public void Die()
        {
            View.Animator.SetTrigger(Died);
            _isAlive = false;
        }

        public void ReachedTarget()
        {
            View.Animator.SetTrigger(Won);
            _isAlive = false;
        }
    }
}