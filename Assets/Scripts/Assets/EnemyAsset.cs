using Enemy;
using UnityEngine;

namespace Assets
{
    public enum MovementType
    {
        Grid,
        Flying
    }

    [CreateAssetMenu(fileName = "Enemy", menuName = "Assets/Enemy Asset", order = 2)]
    public class EnemyAsset : ScriptableObject
    {
        public EnemyView _viewPrefab;
        public MovementType _movementType;
        public int _startHealth;
        public float _speed;
    }
}