using Shooter.Model;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class BulletsFactory : MonoBehaviour, IBulletsFactory
    {
        [SerializeField] private BulletMovement _prefab;
        [SerializeField] private Transform _spawnPoint;
        
        private IndependentPool<BulletMovement> _pool;

        private void Awake()
        {
            var gameObjectsFactory = new GameObjectsFactory<BulletMovement>(_prefab, transform);
            _pool = new IndependentPool<BulletMovement>(gameObjectsFactory);
        }

        public IBullet Create()
        {
            var bullet = _pool.Get();
            bullet.transform.position = _spawnPoint.position;
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        private void Update() => _pool.Update(Time.deltaTime);
        
    }
}