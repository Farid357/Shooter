using System;
using System.Collections.Generic;
using Shooter.Model;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ShotgunBulletsFactory : BulletsFactory
    {
        [SerializeField, Min(2)] private int _bullets = 3;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private float _minZRotation;
        [SerializeField] private float _maxZRotation;
        [SerializeField] private float _xOffset;
        [SerializeField] private Transform _spawnPoint;

        private float _rotationZOffset;
        private IndependentPool<Bullet> _pool;
        
        public override event Action<Bullet> OnCreated;

        private void Awake()
        {
            if (_minZRotation > _maxZRotation)
                throw new ArgumentOutOfRangeException("Min rotation is higher than max!");

            var factory = new GameObjectsFactory<Bullet>(_prefab, transform);
            _pool = new IndependentPool<Bullet>(factory);
            _rotationZOffset = (_maxZRotation - _minZRotation) / _bullets;
        }

        public override IBullet Create()
        {
            var bulletsEnumerable = CreateBullets();
            return new Bullets(bulletsEnumerable);
        }


        private IEnumerable<IBullet> CreateBullets()
        {
            var zRotation = _minZRotation;
            var xPosition = _spawnPoint.position.x;

            for (var i = 0; i < _bullets; i++)
            {
                var bullet = _pool.Get();
                bullet.transform.position = _spawnPoint.position;
                bullet.transform.SetPositionX(xPosition).SetRotationZ(zRotation).SetRotationX(-90);
                bullet.gameObject.SetActive(true);
                xPosition += _xOffset;
                zRotation += _rotationZOffset;
                OnCreated?.Invoke(bullet);
                yield return bullet;
            }
        }
    }
}