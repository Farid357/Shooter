using Shooter.Shop;

namespace Shooter.Test
{
    public sealed class DummyShoppingCartView : IShoppingCartView
    {
        public int TotalPrice { get; private set; }
        
        public void Visualize(IGood good)
        {
            
        }

        public void Remove(IGood good)
        {
        }

        public void Clear()
        {
        }

        public void VisualizeTotalPrice(int totalPrice)
        {
            TotalPrice = totalPrice;
        }
    }
}