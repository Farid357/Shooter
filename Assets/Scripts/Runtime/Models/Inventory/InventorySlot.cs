using System;

namespace Shooter.Model.Inventory
{
    public readonly struct InventorySlot<TItem>
    {
        public readonly IItemSelector<TItem> ItemSelector;
        public readonly Item<TItem> Item;

        public InventorySlot(IItemSelector<TItem> selector, Item<TItem> item)
        {
            Item = item;
            ItemSelector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

    }
}