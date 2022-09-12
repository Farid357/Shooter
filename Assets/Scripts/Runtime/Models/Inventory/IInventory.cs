using Shooter.Model.Inventory;

namespace Shooter.Model
{
    public interface IInventory<T> : IInventoryItemsContainer<T>
    {
        public bool IsFull { get; }
        
        public void Add((IItemSelector<T> Selector, Item<T> Item) slot, int itemCount);
        
    }
}