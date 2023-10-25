using KermansUtility.Patterns.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KermansUtility.Patterns.ObjectPool
{
    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        [SerializeField] private List<PoolObject> _poolObjects = new List<PoolObject>();

        private void Awake()
        {
            for (int i = 0; i < _poolObjects.Count; i++)
            {
                PoolObject poolObject = _poolObjects[i];

                poolObject.PooledObjects = new Queue<GameObject>();
                poolObject.PoolIndex = i;

                for (int j = 0; j < poolObject.PoolSize; j++)
                {
                    GameObject newPoolObj = Instantiate(poolObject.ObjectPrefab, Vector2.zero, Quaternion.identity);
                    newPoolObj.SetActive(false);
                    poolObject.PooledObjects.Enqueue(newPoolObj);
                }
            }
        }
        public GameObject GetPooledObject(int poolIndex)
        {
            GameObject poolObj = _poolObjects[poolIndex].PooledObjects.Dequeue();
            poolObj.transform.position = Vector3.zero;
            poolObj.SetActive(true);
            _poolObjects[poolIndex].PooledObjects.Enqueue(poolObj);
            return poolObj;
        }

    }
}