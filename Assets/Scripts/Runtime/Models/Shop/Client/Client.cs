using System;
using System.Linq;
using Shooter.Model;

namespace Shooter.Shop
{
    public sealed class Client : IClient
    {
        private readonly IShoppingCart _shoppingCart;

        public Client(IWallet wallet, IShoppingCart shoppingCart)
        {
            Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
        }
        
        public IWallet Wallet { get; }

        public IReadOnlyShoppingCart ShoppingCart => _shoppingCart;
        
        public bool CanBuyGoods() => Wallet.CanTake(_shoppingCart.TotalPrice);

        public void BuyGoods()
        {
            if (_shoppingCart.Goods.Count() == 0)
                throw new InvalidOperationException("No goods for buying!");

            if (CanBuyGoods() == false)
                throw new InvalidOperationException("Can't buy items!");

            var totalPrice = _shoppingCart.TotalPrice;
            Wallet.Take(totalPrice);

            foreach (var good in _shoppingCart.Goods)
            {
                good.Use();
            }

            _shoppingCart.Clear();
        }
    }
}