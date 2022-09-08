using System;
using Shooter.Model;
using Shooter.Root;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class StandartEnemyFactory : SerializedMonoBehaviour, IFactory<IEnemyMovement>
    {
        [SerializeField] private ICharacterTransform _character;
        [SerializeField] private IHealthTransformView _characterTransformView;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Enemy _prefab;
        
        private IndependentPool<Enemy> _pool;
        private IWallet _wallet;

        public void Init(ISystemUpdate systemUpdate, IWallet wallet)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _pool = new IndependentPool<Enemy>(new GameObjectsFactory<Enemy>(_prefab, transform));
            systemUpdate.Add(_pool);
        }

        public IEnemyMovement Create()
        {
            var enemy = _pool.Get();
            enemy.transform.position = _spawnPoint.position;
            enemy.gameObject.SetActive(true);
            enemy.Init(_character, _characterTransformView, _wallet);
            return enemy.Movement;
        }
    }
}