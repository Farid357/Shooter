using System;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class PotionPickup : Pickup
    {
        private IInventory<IPotion> _inventory;
        private IInventoryItemSelector<IPotion> _potionSelector;
        private IFactory<(IPotion, IInventoryItemGameObjectView)> _potionFactory;
        private InventorySlot<IPotion> _inventorySlot;

        [field: SerializeField] public ItemData ItemData { get; private set; }

        [field: SerializeField] public MovementAlongSpline Movement { get; private set; }
        
        public void Init(IInventory<IPotion> inventory, InventorySlot<IPotion> inventorySlot)
        {
            _inventorySlot = inventorySlot ?? throw new ArgumentNullException(nameof(inventorySlot));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }
        
        protected override void OnPicked()
        {
            if (_inventory.IsFull)
                return;
            
            _inventory.Add(_inventorySlot);
        }
    }
}