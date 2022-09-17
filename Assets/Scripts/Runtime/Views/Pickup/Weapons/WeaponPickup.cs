using System;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.GameLogic
{
    public abstract class WeaponPickup : Pickup
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private ItemGameObjectView _itemView;

        private IInventory<(IWeapon, IWeaponInput)> _inventory;
        private IInventoryItemSelector<(IWeapon, IWeaponInput)> _selector;
        private (IWeapon Model, IWeaponInput Input) _weapon;

        public void Init(IInventory<(IWeapon, IWeaponInput)> inventory, IInventoryItemSelector<(IWeapon, IWeaponInput)> selector, (IWeapon Model, IWeaponInput Input) weapon)
        {
            _weapon = weapon;
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        protected override void OnPicked()
        {
            if (_inventory.IsFull == false && enabled)
            {
                var weapon = new Item<(IWeapon, IWeaponInput)>(_itemData, _weapon, _itemView);
                var slot = new InventorySlot<(IWeapon, IWeaponInput)>(_selector, weapon);
                _inventory.Add(slot, 1);
                gameObject.SetActive(false);
            }
        }
    }
}