using System;
using UnityEngine;

namespace Shooter.Tools
{
    public sealed class GameObjectsFactory<T> : IFactory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Vector3 _spawnPosition = Vector3.zero;
        
        public GameObjectsFactory(T prefab, Transform parent = null)
        {
            _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
            _parent = parent;
        }

        public GameObjectsFactory(T prefab, Vector3 spawnPosition, Transform parent = null) : this(prefab, parent)
        {
            _spawnPosition = spawnPosition;
        }
        
        public T Create()
        {
            if(_spawnPosition != Vector3.zero)
                return UnityEngine.Object.Instantiate(_prefab, _spawnPosition, Quaternion.identity, _parent);

            return UnityEngine.Object.Instantiate(_prefab, _parent);
        }
    }
}