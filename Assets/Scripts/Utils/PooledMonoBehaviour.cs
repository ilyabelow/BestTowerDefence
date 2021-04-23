using UnityEngine;

namespace Utils
{
    public class PooledMonoBehaviour : MonoBehaviour
    {
        private int _prefabId;
        public int PrefabId => _prefabId;

        public void SetPrefabId(int id)
        {
            _prefabId = id;
        }
        
        public virtual void AwakePooled() {}

    }
}