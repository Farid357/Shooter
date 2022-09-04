using System;
using System.Collections.Generic;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public sealed class Inventory<T> : IInventory<T>
    {
        private readonly IInventoryView _inventoryView;
        private readonly Dictionary<Item<T>, int> _items = new();
        private readonly int _maxItemsCount;

        public Inventory(int maxItemsCount, IInventoryView inventoryView)
        {
            _inventoryView = inventoryView ?? throw new ArgumentNullException(nameof(inventoryView));
            _maxItemsCount = maxItemsCount.TryThrowLessThanOrEqualsToZeroException();
        }

        public bool IsFull => _items.Count >= _maxItemsCount;

        public void Add(Item<T> item, int count)
        {
            if (IsFull)
                throw new InvalidOperationException("Inventory is full!");

            if (Contains(item))
                throw new InvalidOperationException("Inventory icontains this item");

            _items.Add(item, count.TryThrowLessThanOrEqualsToZeroException());
            _inventoryView.VisualizeItem(item.Data, count);
        }

        public bool Contains(Item<T> item) => _items.ContainsKey(item);
    }
}