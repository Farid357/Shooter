using System;
using Shooter.Shop;

namespace Shooter.Model
{
    public sealed class BuyGoodButtonAction : IButtonClickAction
    {
        private readonly IClient _client;
        private readonly INotEnoughMoneyView _notEnoughMoneyView;

        public BuyGoodButtonAction(IClient client, INotEnoughMoneyView notEnoughMoneyView)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _notEnoughMoneyView = notEnoughMoneyView ?? throw new ArgumentNullException(nameof(notEnoughMoneyView));
        }

        public void OnClick()
        {
            if (_client.CanBuyItems())
            {
                _client.BuyItems();
            }

            else
            {
                _notEnoughMoneyView.Visualize(_client.ShoppingCart.TotalPrice, _client.Wallet.Money);
            }
        }
    }
}