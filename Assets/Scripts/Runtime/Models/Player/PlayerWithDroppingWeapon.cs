﻿using System;
using System.Collections.Generic;
using System.Linq;
using Shooter.Model;
using Shooter.Model.Inventory;

namespace Shooter.Root
{
    public sealed class PlayerWithDroppingWeapon<TWeapon> : IUpdateble where TWeapon : IThrowingWeapon
    {
        private readonly TWeapon _weapon;
        private readonly IWeaponInput _input;
        private readonly IInventory<TWeapon> _droppingWeaponsInventory;
        private readonly IInventoryItemsSelector _droppingWeaponSelector;
        private readonly IInventory<(IWeapon, IWeaponInput)> _weaponsInventory;
        private readonly IInventoryItemsSelector _weaponSelector;

        public PlayerWithDroppingWeapon(TWeapon weapon, IWeaponInput input,
            IInventory<TWeapon> droppingWeaponsInventory, IInventory<(IWeapon, IWeaponInput)> weaponsInventory,
            IInventoryItemsSelector droppingWeaponSelector, IInventoryItemsSelector weaponSelector)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _droppingWeaponsInventory = droppingWeaponsInventory ?? throw new ArgumentNullException(nameof(droppingWeaponsInventory));
            _droppingWeaponSelector = droppingWeaponSelector ?? throw new ArgumentNullException(nameof(droppingWeaponSelector));
            _weaponsInventory = weaponsInventory ?? throw new ArgumentNullException(nameof(weaponsInventory));
            _weaponSelector = weaponSelector ?? throw new ArgumentNullException(nameof(weaponSelector));
        }

        public void Update(float deltaTime)
        {
            if (_input.IsPressingLeftMouseButton && _weapon.CanShoot)
            {
                _weapon.Shoot();
                _droppingWeaponSelector.Unselect();
                var slot = _droppingWeaponsInventory.Slots.First(slot => slot.Item.Model.Equals(_weapon));
                _droppingWeaponsInventory.Drop(slot);
                new List<IInventoryItemsSelector> {_droppingWeaponSelector, _weaponSelector}.
                    FindAll(selector => selector.CanUnselect)
                    .ForEach(selector => selector.Unselect());
                
                if (_droppingWeaponsInventory.Slots.Any(s => s == slot))
                {
                    _droppingWeaponSelector.Select(_droppingWeaponsInventory.Slots.ToList().IndexOf(slot));
                }

                else if (_weaponsInventory.Slots.Count() > 0)
                {
                    _weaponSelector.Select(0);
                }
            }
        }
    }
}