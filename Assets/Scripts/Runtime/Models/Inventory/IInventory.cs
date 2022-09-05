using Shooter.Model.Inventory;

namespace Shooter.Model
{
    public interface IInventory<T> : IInventoryItemsContainer<T>
    {
        public bool IsFull { get; }
        
        public void Add(Item<T> item, int count);
        
    }
}