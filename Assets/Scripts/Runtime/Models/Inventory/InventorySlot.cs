using System;

namespace Shooter.Model.Inventory
{
    public readonly struct InventorySlot<TItem>
    {
        public readonly IInventoryItemSelector<TItem> ItemSelector;
        public readonly Item<TItem> Item;

        public InventorySlot(IInventoryItemSelector<TItem> selector, Item<TItem> item)
        {
            Item = item;
            ItemSelector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

    }
}