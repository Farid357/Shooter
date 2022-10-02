using System;
using Shooter.Model;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class StandartBulletsFactory : BulletsFactory
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private BulletMovement _prefab;

        private IndependentPool<BulletMovement> _pool;
        
        public override event Action<BulletMovement> OnCreated;

        private void Awake()
        {
            _pool = new IndependentPool<BulletMovement>(new GameObjectsFactory<BulletMovement>(_prefab, transform));
        }

        public override IBullet Create()
        {
            var bullet = _pool.Get();
            bullet.transform.position = _spawnPoint.position;
            bullet.gameObject.SetActive(true);
            OnCreated?.Invoke(bullet);
            return bullet;
        }

    }
}