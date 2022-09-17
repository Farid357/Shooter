using System;
using System.Linq;

namespace Shooter.Model.Inventory
{
    public sealed class InventoryItemsSelector<TItem> : IInventoryItemsSelector
    {
        private readonly IReadOnlyInventory<TItem> _inventory;
        private Item<TItem> _previousSelectedItem;
        private InventorySlot<TItem> _lastSelectedSlot;
        
        public InventoryItemsSelector(IReadOnlyInventory<TItem> inventory)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public bool CanUnselectLast { get; private set; }

        public async void Select(int index)
        {
            if (CanSelect(index) == false)
                throw new InvalidOperationException(nameof(Select));

            if (_previousSelectedItem.View is not null)
                await _previousSelectedItem.View.Hide();
            
            var slot = _inventory.Slots.ElementAt(index);
            await slot.Item.View.Show();
            slot.ItemSelector.Select(slot.Item.Model);
            _previousSelectedItem = slot.Item;
            _lastSelectedSlot = slot;
            CanUnselectLast = true;
        }

        public void UnselectLast()
        {
            if (CanUnselectLast == false)
                throw new InvalidOperationException("Already unselected!");

            _lastSelectedSlot.Item.View.Hide();
            _lastSelectedSlot.ItemSelector.Unselect();
        }

        public bool CanSelect(int index) => _inventory.Contains(index);
    }
}