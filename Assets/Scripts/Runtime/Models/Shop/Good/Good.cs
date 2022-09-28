using System;

namespace Shooter.Shop
{
    public sealed class Good : IGood
    {
        public Good(GoodData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }
        
        public GoodData Data { get; }

        public void Use()
        {
          
        }
    }
}