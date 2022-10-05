using Shooter.Shop;

namespace Shooter.Test
{
    public sealed class DummyNotEnoughMoneyView : INotEnoughMoneyView
    {
        public bool HasVisualized { get; private set; }
        
        public void Visualize(int needMoney, int currentMoney) => HasVisualized = true;
        
    }
}