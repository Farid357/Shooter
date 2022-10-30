using System;
using System.Collections.Generic;
using Shooter.Tools;
using System.Linq;

namespace Shooter.Model.Inventory
{
    public class Inventory<TItem> : IInventory<TItem>
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

        public void Drop(TItem model)
        {
            var slot = _slots.Find(s => s.Item.Model.Equals(model));
            var slotIndex = _slots.IndexOf(slot);
            
            if (_slots.Contains(slot) == false)
                throw new InvalidOperationException("Inventory doesn't contain this slot!");
            
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