using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class ObjectPool
    {
        private static Dictionary<int, Queue<PooledMonoBehaviour>> _pool = new Dictionary<int, Queue<PooledMonoBehaviour>>();

        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour prefab, Vector3 position, Quaternion rotation, Transform parent)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            var instance = InstantiatePooledImpl(prefab);
            var instanceTransform = instance.transform;
            instanceTransform.parent = parent;
            instanceTransform.position = position;
            instanceTransform.rotation = rotation;
            return instance;
        }

        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour prefab)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            return InstantiatePooled(prefab, Vector3.zero, Quaternion.identity, null);
        }
        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour prefab, Transform parent)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            return InstantiatePooled(prefab, Vector3.zero, Quaternion.identity, parent);
        }
        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour prefab, Vector3 position, Quaternion rotation)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            return InstantiatePooled(prefab, position, rotation, null);
        }

        private static TMonoBehaviour InstantiatePooledImpl<TMonoBehaviour>(TMonoBehaviour prefab)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            TMonoBehaviour instance = null;

            int id = prefab.GetInstanceID();
            if (_pool.TryGetValue(id, out var queue))
            {
                if (queue.Count != 0)
                {
                    instance = queue.Dequeue() as TMonoBehaviour;
                    if (instance == null)
                    {
                        throw new NullReferenceException();
                    }
                    instance.gameObject.SetActive(true);
                }
            }
            if (instance == null)
            {
                instance = Object.Instantiate(prefab);
                instance.SetPrefabId(id);
            }
            instance.AwakePooled();

            return instance;
        }

        public static void DestroyPooled(PooledMonoBehaviour instance)
        {
            int id = instance.PrefabId;

            if (_pool.TryGetValue(id, out var queue))
            {
                queue.Enqueue(instance);
            }
            else
            {
                var queueNew = new Queue<PooledMonoBehaviour>();
                queueNew.Enqueue(instance);
                _pool.Add(id, queueNew);
            }

            instance.gameObject.SetActive(false);
        }
    }
}