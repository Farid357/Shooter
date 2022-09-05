using Shooter.Model;
using Shooter.Root;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class StandartBulletsFactory : BulletsFactory
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private BulletMovement _prefab;
        
        private IndependentPool<BulletMovement> _pool;

        public override void Init(ISystemUpdate systemUpdate)
        {
            _pool = new IndependentPool<BulletMovement>(new GameObjectsFactory<BulletMovement>(_prefab, transform));
            systemUpdate.Add(_pool);
        }

        public override IBullet Create()
        {
            var bullet = Instantiate(_prefab);
            bullet.transform.position = _spawnPoint.position;
            bullet.gameObject.SetActive(true);
            return bullet;
        }
    }
}