using System;
using System.Linq;

namespace Shooter.Model.Inventory
{
    public sealed class InventoryItemsSelector<TItem> : IInventoryItemsSelector
    {
        private readonly IReadOnlyInventory<TItem> _inventory;
        private readonly IInventoryItemsSelector _itemsSelector;
        private Item<TItem> _previousSelectedItem;
        private InventorySlot<TItem> _lastSelectedSlot;

        public InventoryItemsSelector(IReadOnlyInventory<TItem> inventory, IInventoryItemsSelector itemsSelector)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _itemsSelector = itemsSelector ?? throw new ArgumentNullException(nameof(itemsSelector));
        }

        public bool CanUnselect { get; private set; }

        public async void Select(int index)
        {
            if (CanSelect(index) == false)
                throw new InvalidOperationException(nameof(Select));

            if (_previousSelectedItem.View is not null)
                await _previousSelectedItem.View.Hide();

            if (_itemsSelector.CanUnselect)
                _itemsSelector.Unselect();

            var slot = _inventory.Slots.ElementAt(index);
            await slot.Item.View.Show();
            slot.ItemSelector.Select(slot.Item.Model);
            _previousSelectedItem = slot.Item;
            _lastSelectedSlot = slot;
            CanUnselect = true;
        }

        public void Unselect()
        {
            if (CanUnselect == false)
                throw new InvalidOperationException("Already unselected!");

            if (_itemsSelector.CanUnselect)
                _itemsSelector.Unselect();
            
            _lastSelectedSlot.Item.View.Hide();
            _lastSelectedSlot.ItemSelector.Unselect();
            CanUnselect = false;
        }

        public bool CanSelect(int index) => _inventory.Contains(index);
    }
}