using System;
using Shooter.Model;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class StandartBulletsFactory : BulletsFactory
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Bullet _prefab;

        private IndependentPool<Bullet> _pool;
        
        public override event Action<Bullet> OnCreated;

        private void Awake()
        {
            _pool = new IndependentPool<Bullet>(new GameObjectsFactory<Bullet>(_prefab, transform));
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