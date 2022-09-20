namespace Shooter.Model.Inventory
{
    public interface IInventoryItemsSelector
    {
        void Select(int index);

        void Unselect();
        
        bool CanSelect(int index);
        
        bool CanUnselect { get; }

    }
}