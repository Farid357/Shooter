namespace Shooter.Model.Inventory
{
    public interface IInventory<TItem> : IReadOnlyInventory<TItem>
    {
        public bool IsFull { get; }
        
        public void Add(InventorySlot<TItem> slot);
    }
}