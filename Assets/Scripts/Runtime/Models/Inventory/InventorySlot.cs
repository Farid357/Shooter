using System;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public sealed class InventorySlot<TItem>
    {
        public InventorySlot(IInventoryItemSelector<TItem> selector, Item<TItem> item, int itemsCount)
        {
            Item = item;
            ItemsCount = itemsCount.TryThrowLessThanOrEqualsToZeroException();
            ItemSelector = selector ?? throw new ArgumentNullException(nameof(selector));
        }
        
        public IInventoryItemSelector<TItem> ItemSelector { get; }
        public Item<TItem> Item { get; }
        
        public int ItemsCount { get; private set; }

        public void DropOneItem()
        {
            if (CanDropOneItem() == false)
                throw new InvalidOperationException(nameof(DropOneItem));

            ItemsCount--;
        }

        public bool CanDropOneItem() => ItemsCount - 1 > 0;
    }
}