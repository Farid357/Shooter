using Shooter.Model;
using Shooter.Root;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class StandartEnemyFactory : SerializedMonoBehaviour, IFactory<IEnemyMovement>
    {
        [SerializeField] private ICharacter _character;
        [SerializeField] private IHealthTransformView _characterTransformView;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Enemy _prefab;
        
        private IndependentPool<Enemy> _pool;

        public void Init(ISystemUpdate systemUpdate)
        {
            _pool = new IndependentPool<Enemy>(new GameObjectsFactory<Enemy>(_prefab, transform));
            systemUpdate.Add(_pool);
        }

        public IEnemyMovement Create()
        {
            var enemy = _pool.Get();
            enemy.transform.position = _spawnPoint.position;
            enemy.gameObject.SetActive(true);
            enemy.Init(_character, _characterTransformView);
            return enemy.Movement;
        }
    }
}