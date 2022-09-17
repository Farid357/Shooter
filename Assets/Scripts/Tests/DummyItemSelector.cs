using Shooter.Model.Inventory;

namespace Shooter.Test
{
    public sealed class DummyItemSelector<T> : IInventoryItemSelector<T>
    {
        public void Select(T grenade)
        {
            
        }

        public void Unselect()
        {
            
        }
    }
}