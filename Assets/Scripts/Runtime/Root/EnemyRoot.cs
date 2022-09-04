using Shooter.GameLogic;
using Shooter.Tools;
using Sirenix.Utilities;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class EnemyRoot : CompositeRoot
    {
        [SerializeField] private StandartEnemyFactory[] _factories;
        [SerializeField] private CharacterMovement _character;
        [SerializeField] private Enemy _enemy;
        private SystemUpdate _systemUpdate;

        public override void Compose()
        {
            _systemUpdate = new SystemUpdate();
            _factories.ForEach(factory => factory.Init(_systemUpdate));
            new NavMeshBaker().Bake();
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