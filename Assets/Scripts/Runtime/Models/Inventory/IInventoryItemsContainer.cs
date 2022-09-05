using System.Collections.Generic;

namespace Shooter.Model.Inventory
{
    public interface IInventoryItemsContainer<T>
    {
        public IEnumerable<Item<T>> Items { get; }

        public bool Contains(int index);
        
    }
}