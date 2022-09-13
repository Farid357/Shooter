using System;
using System.Linq;

namespace Shooter.Model.Inventory
{
    public sealed class InventoryItemsSelector<TItem> : IInventoryItemsSelector
    {
        private readonly IReadOnlyInventory<TItem> _inventory;
        private Item<TItem> _lastSelectedItem;

        public InventoryItemsSelector(IReadOnlyInventory<TItem> inventory)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public void Select(int index)
        {
            if (CanSelect(index) == false)
                throw new InvalidOperationException(nameof(Select));
            
            _lastSelectedItem.View?.Hide();
            var slot = _inventory.Slots.ElementAt(index);
            slot.Item.View.Show();
            slot.ItemSelector.Select(slot.Item.Model);
            _lastSelectedItem = slot.Item;
        }

        public bool CanSelect(int index) => _inventory.Contains(index);
    }
}