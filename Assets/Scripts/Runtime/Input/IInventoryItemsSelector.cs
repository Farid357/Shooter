namespace Shooter.Model.Inventory
{
    public interface IInventoryItemsSelector
    {
        void Select(int index);
        
        bool CanSelect(int index);
    }
}