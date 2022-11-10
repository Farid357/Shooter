using System;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public sealed class InventorySlot<TItem>
    {
        public InventorySlot(IInventoryItemSelector<TItem> selector, Item<TItem> item, int maxItemsCount, int itemsCount = 1)
        {
            if (maxItemsCount < itemsCount)
                throw new ArgumentOutOfRangeException(nameof(maxItemsCount));
            
            Item = item;
            MaxItemsCount = maxItemsCount.TryThrowLessThanOrEqualsToZeroException();
            ItemsCount = itemsCount.TryThrowLessThanOrEqualsToZeroException();
            ItemSelector = selector ?? throw new ArgumentNullException(nameof(selector));
        }
        
        public IInventoryItemSelector<TItem> ItemSelector { get; }
        
        public Item<TItem> Item { get; }
        
        public int MaxItemsCount { get; }

        public int ItemsCount { get; private set; }

        public void DropOneItem()
        {
            if (CanDropOneItem() == false)
                throw new InvalidOperationException(nameof(DropOneItem));

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