using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using Shooter.Tools;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shooter.Root
{
    public sealed class EnemyRoot : CompositeRoot
    {
        [FormerlySerializedAs("_factories")] [SerializeField] private BulletsFactory[] _bulletsFactories;
        [SerializeField] private NavMeshBaker _navMeshBaker;
        [SerializeField] private StandartEnemyFactory _enemyFactory;
        [SerializeField] private ICharacterMovement _character;
        [SerializeField] private IView<int> _moneyView;
        [SerializeField] private IHealthTransformView _characterHealthTransformView;

        [SerializeField] private Enemy _enemy;
        private SystemUpdate _systemUpdate;

        public override void Compose()
        {
            var wallet = new Wallet(_moneyView, new BinaryStorage());
            _enemy.Init(_character, _characterHealthTransformView.Health, _characterHealthTransformView, wallet);
            _systemUpdate = new SystemUpdate();
            _enemyFactory.Init(_systemUpdate, wallet);
            _bulletsFactories.ForEach(factory => factory.Init(_systemUpdate));
            _navMeshBaker.Bake();
            var b = _enemy.GetComponent<StandartEnemyMovement>();
            b.MoveToCharacter();
            b.RotateToCharacter();
            // UniTask.Create(async () =>
            // {
            //     foreach (var factory in _factories)
            //     {
            //         await UniTask.Delay(TimeSpan.FromSeconds(1f));
            //         var enemy = factory.Create();
            //         enemy.RotateToCharacter();
            //         enemy.MoveToCharacter();
            //         _navMeshBaker.Bake();
            //     }
            // });
        }

        private void Update() => _systemUpdate.Update(Time.deltaTime);
        
    }
}