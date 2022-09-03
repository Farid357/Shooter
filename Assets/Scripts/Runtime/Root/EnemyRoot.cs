using System;
using Cysharp.Threading.Tasks;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class EnemyRoot : CompositeRoot
    {
        [SerializeField] private EnemyFactory[] _factories;
        [SerializeField] private CharacterMovement _character;
        [SerializeField] private NavMeshBaker _navMeshBaker;
        [SerializeField] private Enemy _enemy;
        
        public override void Compose()
        {
            _enemy.Init(_character, _character.GetComponent<HealthCollision>());
            var b = _enemy.GetComponent<EnemyMovement>();
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
    }
}