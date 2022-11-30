using System;
using System.Linq;
using Shooter.Tools;

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

        public bool CanUnselect { get; private set; }

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
            CanUnselect = true;
        }

        public void Unselect()
        {
            if (CanUnselect == false)
                throw new InvalidOperationException("Already unselected!");

            if (_lastSelectedSlot.Item.Model.IsNotThrowingWeapon())
                _lastSelectedSlot.Item.View.Hide();
            _lastSelectedSlot.ItemSelector.Unselect();
            CanUnselect = false;
        }

        public bool CanSelect(int index) => _inventory.Slots.Count() > index;
    }
}