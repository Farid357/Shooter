﻿using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class GrenadeSelectorRoot : SerializedMonoBehaviour
    {
        [SerializeField] private IPlayerRoot _playerRoot;
        [SerializeField] private GameObjectFactory<GrenadeView, CharacterMovement> _grenadesFactory;
        [SerializeField] private IBulletsView[] _bulletsViews;
        private IInventoryItemSelector<IGrenade> _grenadesSelector;

        public IInventoryItemSelector<IGrenade> Compose()
        {
            return _grenadesSelector ??= new DroppingWeaponSelector(_playerRoot, _grenadesFactory, _bulletsViews);
        }
    }
}