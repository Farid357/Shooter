using System;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class GrenadePickup : Pickup
    {
        [SerializeField, Min(1)] private int _count = 1;
        [SerializeField] private ItemData _itemData;

        private IInventory<IGrenade> _inventory;
        private IFactory<IGrenade> _factory;
        private IInventoryItemSelector<IGrenade> _selector;

        public void Init(IInventory<IGrenade> inventory, IInventoryItemSelector<IGrenade> selector, IFactory<IGrenade> factory)
        {
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override void OnPicked()
        {
            if (_inventory.IsFull == false)
            {
                var grenade = _factory.Create();
                var item = new Item<IGrenade>(_itemData, grenade, grenade.ItemView);
                var slot = new InventorySlot<IGrenade>(_selector, item, _count);
                _inventory.Add(slot);
            }
        }
    }
}