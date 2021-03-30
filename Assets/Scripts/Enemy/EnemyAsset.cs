using UnityEngine;

namespace Enemy
{
    public enum MovementType
    {
        Grid,
        Flying
    }

    [CreateAssetMenu(fileName = "Enemy", menuName = "Assets/Enemy Asset")]
    public class EnemyAsset : ScriptableObject
    {
        public EnemyView EnemyPrefab;
        public MovementType MovementType;
        public int StartHealth;
        public float Speed;
    }
}