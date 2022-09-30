using System;
using System.Linq;
using Shooter.Model;

namespace Shooter.Shop
{
    public sealed class Client : IClient
    {
        private readonly INotEnoughMoneyView _notEnoughMoneyView;
        private readonly IWallet _wallet;
        private readonly IShoppingCart _shoppingCart;

        public Client(INotEnoughMoneyView notEnoughMoneyView, IWallet wallet, IShoppingCart shoppingCart)
        {
            _notEnoughMoneyView = notEnoughMoneyView ?? throw new ArgumentNullException(nameof(notEnoughMoneyView));
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
        }

        public void BuyItems()
        {
            if (_shoppingCart.Goods.Count() == 0)
                throw new InvalidOperationException("No goods for buying!");

            var totalPrice = _shoppingCart.TotalPrice;
            if (_wallet.CanTake(totalPrice))
            {
                _wallet.Take(totalPrice);
                
                foreach (var good in _shoppingCart.Goods)
                {
                    good.Use();
                }
                _shoppingCart.Clear();
            }

            else
            {
                _notEnoughMoneyView.Visualize(totalPrice, _wallet.Money);
            }
        }
    }
}