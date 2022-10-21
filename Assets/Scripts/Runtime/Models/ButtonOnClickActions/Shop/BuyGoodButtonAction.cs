using System;
using System.Collections.Generic;
using Shooter.Shop;

namespace Shooter.Model
{
    public sealed class BuyGoodButtonAction : IButtonClickAction
    {
        private readonly IEnumerable<IClient> _clients;
        private readonly INotEnoughMoneyView _notEnoughMoneyView;

        public BuyGoodButtonAction(IEnumerable<IClient> clients, INotEnoughMoneyView notEnoughMoneyView)
        {
            _clients = clients ?? throw new ArgumentNullException(nameof(clients));
            _notEnoughMoneyView = notEnoughMoneyView ?? throw new ArgumentNullException(nameof(notEnoughMoneyView));
        }

        public void OnClick()
        {
            foreach (var client in _clients)
            {
                if (client.CanBuyItems())
                {
                    client.BuyItems();
                }

                else
                {
                    _notEnoughMoneyView.Visualize(client.ShoppingCart.TotalPrice, client.Wallet.Money);
                }
            }
        }
    }
}