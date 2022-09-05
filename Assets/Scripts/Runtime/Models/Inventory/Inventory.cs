using System;
using System.Collections.Generic;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public sealed class Inventory<T> : IInventory<T>
    {
        private readonly IInventoryView _inventoryView;
        private readonly List<Item<T>> _items = new();

        private const int MaxItemsCount = 10;

        public Inventory(IInventoryView inventoryView)
        {
            _inventoryView = inventoryView ?? throw new ArgumentNullException(nameof(inventoryView));
        }

        public IEnumerable<Item<T>> Items => _items;

        public bool IsFull => _items.Count >= MaxItemsCount;

        public void Add(Item<T> item, int count)
        {
            if (IsFull)
                throw new InvalidOperationException("Inventory is full!");

            if (_items.Contains(item))
                throw new InvalidOperationException("Inventory is already contains this item");

            count.TryThrowLessThanOrEqualsToZeroException();
            _items.Add(item);
            _inventoryView.VisualizeItem(item.Data, count);
        }

        public bool Contains(int index) => _items.Count < index.TryThrowLessThanOrEqualsToZeroException();
    }
}