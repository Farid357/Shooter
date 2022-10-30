using Shooter.Model;
using Shooter.Shop;
using UnityEngine;

namespace Shooter.Test
{
    public sealed class DummyGoodData : IGoodData
    {
        public string Name => "Good";
        
        public Sprite Sprite { get; }

        public int Price => 500;
        
        public WalletType WalletForPay => WalletType.WithCoins;
    }
}