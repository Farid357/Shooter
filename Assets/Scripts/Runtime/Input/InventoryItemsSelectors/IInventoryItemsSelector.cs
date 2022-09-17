namespace Shooter.Model.Inventory
{
    public interface IInventoryItemsSelector
    {
        void Select(int index);

        void UnselectLast();
        
        bool CanSelect(int index);
        
        bool CanUnselectLast { get; }

    }
}