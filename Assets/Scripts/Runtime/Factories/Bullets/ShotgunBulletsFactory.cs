using System;
using System.Collections.Generic;
using Shooter.Model;
using Shooter.Root;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ShotgunBulletsFactory : BulletsFactory
    {
        [SerializeField, Min(2)] private int _bullets = 3;
        [SerializeField] private BulletMovement _prefab;
        [SerializeField] private float _minZRotation;
        [SerializeField] private float _maxZRotation;
        [SerializeField] private float _xOffset;
        [SerializeField] private Transform _spawnPoint;

        private float _rotationZOffset;
        private IndependentPool<BulletMovement> _pool;

        public override void Init(ISystemUpdate systemUpdate)
        {
            if (_minZRotation > _maxZRotation)
                throw new ArgumentOutOfRangeException("Min rotation is higher than max!");

            var factory = new GameObjectsFactory<BulletMovement>(_prefab, transform);
            _pool = new IndependentPool<BulletMovement>(factory);
            _rotationZOffset = (_maxZRotation - _minZRotation) / _bullets;
            systemUpdate.Add();
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
                bullet.gameObject.SetActive(true);
                bullet.transform.SetXPosition(xPosition);
                bullet.transform.SetZRotation(zRotation);
                xPosition += _xOffset;
                zRotation += _rotationZOffset;
                yield return bullet;
            }
        }
    }
}