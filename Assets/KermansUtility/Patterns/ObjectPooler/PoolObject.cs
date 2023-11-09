using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KermansUtility.Patterns.ObjectPool
{
    [CreateAssetMenu(fileName ="Pool Object",menuName ="Kermans Utility/Patterns/Pool Object")]
    public class PoolObject : ScriptableObject
    {
        public GameObject ObjectPrefab => _objectPrefab;
        public Queue<GameObject> PooledObjects { get; set; }
        public int PoolIndex { get; set; }
        public int PoolSize => _poolSize;

        [SerializeField] private GameObject _objectPrefab;
        [SerializeField] private int _poolSize;
    }
}