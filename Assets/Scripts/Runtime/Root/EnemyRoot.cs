﻿using System;
using Cysharp.Threading.Tasks;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using Shooter.Tools;
using Sirenix.Utilities;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class EnemyRoot : CompositeRoot
    {
        [SerializeField] private BulletsFactory[] _factories;
        [SerializeField] private NavMeshBaker _navMeshBaker;
        [SerializeField] private StandartEnemyFactory _enemyFactory;
        [SerializeField] private ICharacterTransform _character;
        [SerializeField] private IHealthTransformView _healthTransformView;
        [SerializeField] private IView<int> _moneyView;
        
        [SerializeField] private Enemy _enemy;
        private SystemUpdate _systemUpdate;

        public override void Compose()
        {
            var wallet = new Wallet(_moneyView, new BinaryStorage());
            _enemy.Init(_character, _healthTransformView, wallet);
            _systemUpdate = new SystemUpdate();
            _enemyFactory.Init(_systemUpdate, wallet);
            _factories.ForEach(factory => factory.Init(_systemUpdate));
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