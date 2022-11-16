using System;
using System.Collections.Generic;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class GrenadePickup : Pickup
    {
        [SerializeField, Min(1)] private int _count = 2;
        [SerializeField, Min(1)] private int _maxItemsCountInSlot = 2;
        [SerializeField] private ItemData _itemData;

        private IInventory<IThrowingWeapon> _inventory;
        private IFactory<IThrowingWeapon> _factory;
        private IInventoryItemSelector<IThrowingWeapon> _selector;

        public void Init(IInventory<IThrowingWeapon> inventory, IInventoryItemSelector<IThrowingWeapon> selector, IFactory<IThrowingWeapon> factory)
        {
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override void OnPicked()
        {
            if (_inventory.IsFull == false)
            {
                var slot = new InventorySlot<IThrowingWeapon>(_selector, CreateItems(), _maxItemsCountInSlot);
                _inventory.Add(slot);
            }
        }

        private IEnumerable<Item<IThrowingWeapon>> CreateItems()
        {
            for (var i = 0; i < _count; i++)
            {
                var grenade = _factory.Create();
                var item = new Item<IThrowingWeapon>(_itemData, grenade, grenade.ItemView);
                yield return item;
            }
        }
    }
}