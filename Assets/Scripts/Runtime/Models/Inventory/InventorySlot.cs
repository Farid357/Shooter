using System;
using System.Collections.Generic;
using System.Linq;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public sealed class InventorySlot<TItem>
    {
        private readonly List<Item<TItem>> _items = new();
        
        public InventorySlot(IInventoryItemSelector<TItem> selector, Item<TItem> item) : this(selector, new []{item}, 1) { }

        public InventorySlot(IInventoryItemSelector<TItem> selector, IEnumerable<Item<TItem>> items, int maxItemsCount)
        {
            ItemsCount = items.Count();
            
            if (maxItemsCount < ItemsCount)
                throw new ArgumentOutOfRangeException(nameof(maxItemsCount));
            
            ItemSelector = selector ?? throw new ArgumentNullException(nameof(selector));
            MaxItemsCount = maxItemsCount.TryThrowLessThanOrEqualsToZeroException();
            _items.AddRange(items);
        }
        
        public IInventoryItemSelector<TItem> ItemSelector { get; }

        public Item<TItem> Item => _items[^1];

        public int MaxItemsCount { get; }

        public int ItemsCount { get; private set; }

        public void DropOneItem()
        {
            if (CanDropOneItem() == false)
                throw new InvalidOperationException(nameof(DropOneItem));

            _items.RemoveAt(_items.Count - 1);
            ItemsCount--;
        }

        public void AddItems(int count)
        {
            if (MaxItemsCount < ItemsCount + count)
                throw new InvalidOperationException("Trying add more items than max!");
            
            ItemsCount += count.TryThrowLessThanOrEqualsToZeroException();
        }
        
        private bool CanDropOneItem() => ItemsCount - 1 >= 0;
    }
}