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
        [SerializeField] private GameObjectFactory<ThrowingWeapon, CharacterMovement> _grenadesFactory;
        [SerializeField] private IBulletsView[] _bulletsViews;
        private IInventoryItemSelector<IThrowingWeapon> _grenadesSelector;

        public IInventoryItemSelector<IThrowingWeapon> Compose()
        {
            return _grenadesSelector ??= new ThrowingWeaponSelector(_playerRoot, _bulletsViews);
        }
    }
}