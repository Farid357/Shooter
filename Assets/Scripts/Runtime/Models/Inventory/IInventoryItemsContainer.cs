using System.Collections.Generic;

namespace Shooter.Model.Inventory
{
    public interface IInventoryItemsContainer<T>
    {
        public IEnumerable<(IItemSelector<T> Selector, Item<T> Item)> Slots { get; }

        public bool Contains(int index);
    }
}