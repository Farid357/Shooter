namespace Shooter.Model.Inventory
{
    public sealed class DummyItemsSelector : IInventoryItemsSelector
    {
        public bool CanUnselect => true;
        
        public void Select(int index)
        {
            
        }

        public void Unselect()
        {
        }

        public bool CanSelect(int index) => true;
        
    }
}