namespace Enemy
{
    public class EnemyData
    {
        private EnemyView _view;
        public EnemyView View => _view;

        private bool _isAlive = true;
        public bool IsAlive => _isAlive;
        
        private float _health;


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
            _isAlive = false;
        }
    }
}