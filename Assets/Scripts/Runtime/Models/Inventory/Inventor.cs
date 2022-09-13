using System;
using System.Collections.Generic;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public sealed class Inventory<TItem> : IInventory<TItem>
    {
        private readonly IInventoryView _view;
        private readonly List<InventorySlot<TItem>> _slots = new();

        private const int MaxItemsCount = 10;

        public Inventory(IInventoryView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public IEnumerable<InventorySlot<TItem>> Slots => _slots;

        public bool IsFull => _slots.Count >= MaxItemsCount;

        public void Add(InventorySlot<TItem> slot, int itemCount)
        {
            if (IsFull)
                throw new InvalidOperationException("Inventory is full!");

            if (_slots.Contains(slot))
                throw new InvalidOperationException("Inventory already contains this item");

            itemCount.TryThrowLessThanOrEqualsToZeroException();
            _view.VisualizeItem(slot.Item.Data, itemCount);
            _slots.Add(slot);
        }

        public bool Contains(int index) => _slots.Count > index;
    }
}