using System;
using System.Linq;
using Shooter.GameLogic;

namespace Shooter.Model.Inventory
{
    public sealed class InventoryItemsDrop : IUpdateble
    {
        private readonly IInventory<IGrenade> _inventory;

        public InventoryItemsDrop(IInventory<IGrenade> inventory)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public void Update(float deltaTime)
        {
            foreach (var slot in _inventory.Slots.ToList())
            {
                var weapon = slot.Item.Model;

                if (weapon.HasShot)
                {
                    _inventory.Drop(slot);
                }
            }
        }
    }
}