using Shooter.Model;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class StandartEnemyFactory : EnemyFactory
    {
        [SerializeField] private ICharacter _character;
        [SerializeField] private IHealthCollision _characterCollision;
        [SerializeField] private Enemy _prefab;
        [SerializeField] private Transform _spawnPoint;
        private IndependentPool<Enemy> _pool;
        
        private void Awake()
        {
            var factory = new GameObjectsFactory<Enemy>(_prefab, transform);
            _pool = new IndependentPool<Enemy>(factory);
        }

        public override IEnemyMovement Create()
        {
            var enemy = _pool.Get();
            enemy.transform.position = _spawnPoint.position;
            enemy.gameObject.SetActive(true);
            enemy.Init(_character, _characterCollision);
            return enemy.Movement;
        }
    }
}