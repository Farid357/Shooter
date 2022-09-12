using Shooter.Model.Inventory;

namespace Shooter.Test
{
    public sealed class DummyItemSelector<T> : IItemSelector<T>
    {
        public void Select(T item)
        {
            
        }
    }
}