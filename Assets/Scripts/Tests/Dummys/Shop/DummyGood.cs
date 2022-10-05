using Shooter.Shop;

namespace Shooter.Test
{
    public sealed class DummyGood : IGood
    {
        public IGoodData Data => new DummyGoodData();
        
        public void Use()
        {

        }
    }
}