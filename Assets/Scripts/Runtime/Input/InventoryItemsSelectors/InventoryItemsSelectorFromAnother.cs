using System;
using System.Linq;

namespace Shooter.Model.Inventory
{
    public sealed class InventoryItemsSelectorFromAnother<TItem> : IInventoryItemsSelector
    {
        private readonly IReadOnlyInventory<TItem> _inventory;
        private readonly IInventoryItemsSelector _itemsSelector;
        private Item<TItem> _previousSelectedItem;
        
        public InventoryItemsSelectorFromAnother(IReadOnlyInventory<TItem> inventory, IInventoryItemsSelector itemsSelector)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _itemsSelector = itemsSelector ?? throw new ArgumentNullException(nameof(itemsSelector));
        }

        public bool CanUnselectLast => false;

        public async void Select(int index)
        {
            if (CanSelect(index) == false)
                throw new InvalidOperationException(nameof(Select));

            if (_previousSelectedItem.View is not null)
                await _previousSelectedItem.View.Hide();
            
            _itemsSelector.UnselectLast();
            var slot = _inventory.Slots.ElementAt(index);
            await slot.Item.View.Show();
            slot.ItemSelector.Select(slot.Item.Model);
            _previousSelectedItem = slot.Item;
        }

        public void UnselectLast() => throw new InvalidOperationException(nameof(UnselectLast));

        public bool CanSelect(int index) => _inventory.Contains(index);
    }
}