using System;
using System.Linq;

namespace Shooter.Model.Inventory
{
    public sealed class InventoryItemsDrop<TItem> : IUpdateble where TItem : IDroppingWeapon
    {
        private readonly IInventory<TItem> _inventory;

        public InventoryItemsDrop(IInventory<TItem> inventory)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public void Update(float deltaTime)
        {
            foreach (var slot in _inventory.Slots.ToList())
            {
                var weapon = slot.Item.Model;

                if (weapon.HasDropped)
                {
                    _inventory.Drop(slot);
                }
            }
        }
    }
}