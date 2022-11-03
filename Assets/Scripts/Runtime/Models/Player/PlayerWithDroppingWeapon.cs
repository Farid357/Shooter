using System;
using System.Linq;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Model.Inventory;

namespace Shooter.Root
{
    public sealed class PlayerWithDroppingWeapon<TWeapon> : IUpdateble where TWeapon : IDroppingWeapon
    {
        private readonly TWeapon _weapon;
        private readonly IWeaponInput _input;
        private readonly IInventory<TWeapon> _droppingWeaponsInventory;
        private readonly IInventoryItemSelector<TWeapon> _droppingWeaponSelector;
        private readonly IInventory<(IWeapon, IWeaponInput)> _weaponsInventory;
        private readonly IInventoryItemSelector<(IWeapon, IWeaponInput)> _weaponSelector;

        public PlayerWithDroppingWeapon(TWeapon weapon, IWeaponInput input, IInventory<TWeapon> droppingWeaponsInventory,  IInventory<(IWeapon, IWeaponInput)> weaponsInventory,
            IInventoryItemSelector<TWeapon> droppingWeaponSelector, IInventoryItemSelector<(IWeapon, IWeaponInput)> weaponSelector)
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
                var slot = _droppingWeaponsInventory.Slots.First(slot => slot.Item.Model.Equals(_weapon));
                _droppingWeaponsInventory.Drop(slot);

                if (_droppingWeaponsInventory.Slots.Contains(slot))
                {
                    _droppingWeaponSelector.Select(slot.Item.Model);
                }
                
                else if(_weaponsInventory.Slots.Count() > 0)
                {
                    var firstWeapon = _weaponsInventory.Slots.ElementAt(0).Item.Model;
                    _weaponSelector.Select(firstWeapon);
                }
            }
        }
    }
}