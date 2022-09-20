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

            _view.VisualizeItem(slot.Item.Data, slot.ItemsCount);
            _slots.Add(slot);
        }

        public bool Contains(int index) => _slots.Count > index;
    }
}