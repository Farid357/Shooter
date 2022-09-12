using System;
using System.Collections.Generic;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public sealed class Inventory<T> : IInventory<T>
    {
        private readonly IInventoryView _inventoryView;
        private readonly List<(IItemSelector<T> Selector, Item<T> Item)> _slots = new();

        private const int MaxItemsCount = 10;

        public Inventory(IInventoryView inventoryView)
        {
            _inventoryView = inventoryView ?? throw new ArgumentNullException(nameof(inventoryView));
        }

        public IEnumerable<(IItemSelector<T> Selector, Item<T> Item)> Slots => _slots;

        public bool IsFull => _slots.Count >= MaxItemsCount;

        public void Add((IItemSelector<T> Selector, Item<T> Item) slot, int itemCount)
        {
            if (IsFull)
                throw new InvalidOperationException("Inventory is full!");

            if (_slots.Contains(slot))
                throw new InvalidOperationException("Inventory is already contains this item");

            itemCount.TryThrowLessThanOrEqualsToZeroException();
            _slots.Add(slot);
            _inventoryView.VisualizeItem(slot.Item.Data, itemCount);
        }

        public bool Contains(int index) => _slots.Count > index;
    }

    public interface IItemSelector<T>
    {
        public void Select(T item);
    }
}