using System;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public readonly struct InventorySlot<TItem>
    {
        public readonly IInventoryItemSelector<TItem> ItemSelector;
        public readonly Item<TItem> Item;
        public readonly int ItemsCount;
        
        public InventorySlot(IInventoryItemSelector<TItem> selector, Item<TItem> item, int itemsCount)
        {
            Item = item;
            ItemsCount = itemsCount.TryThrowLessThanOrEqualsToZeroException();
            ItemSelector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

    }
}