using Shooter.Model.Inventory;

namespace Shooter.Model
{
    public interface IInventory<T>
    {
        public bool IsFull { get; }
        
        public void Add(Item<T> item, int count);
        
        public bool Contains(Item<T> item);
    }
}