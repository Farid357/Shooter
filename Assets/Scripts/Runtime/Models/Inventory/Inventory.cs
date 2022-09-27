using System;
using System.Collections.Generic;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public sealed class Inventory<TItem> : IInventory<TItem>
    {
        private readonly IInventoryView _view;
        private readonly int _maxSlotsCount;
        private readonly List<InventorySlot<TItem>> _slots = new();

        public Inventory(IInventoryView view, int maxSlotsCount = 10)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _maxSlotsCount = maxSlotsCount.TryThrowLessThanOrEqualsToZeroException();
        }

        public IEnumerable<InventorySlot<TItem>> Slots => _slots;

        public bool IsFull => _slots.Count >= _maxSlotsCount;
        
        public void Add(InventorySlot<TItem> slot)
        {
            if (IsFull)
                throw new InvalidOperationException("Inventory is full!");

            _view.VisualizeNewItem(slot.Item.Data, slot.ItemsCount);
            _slots.Add(slot);
        }

        public void Drop(InventorySlot<TItem> slot)
        {
            if (_slots.Contains(slot) == false)
                throw new InvalidOperationException("Inventory doesn't contain this slot!");

            var slotIndex = _slots.IndexOf(slot);

            if (slot.CanDropOneItem())
            {
                _slots[slotIndex].DropOneItem();
                _view.VisualizeItemsCount(_slots[slotIndex].Item.Data, _slots[slotIndex].ItemsCount);
            }

            else
            {
                _view.DropItem(_slots[slotIndex].Item.Data);
                _slots.RemoveAt(slotIndex);
            }
        }
    }
}