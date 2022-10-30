using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shooter.Shop;

namespace Shooter.Model
{
    public sealed class BuyGoodsButtonAction : IButtonClickAction
    {
        private readonly IEnumerable<IClient> _clients;
        private readonly INotEnoughMoneyView _notEnoughMoneyView;

        public BuyGoodsButtonAction(IEnumerable<IClient> clients, INotEnoughMoneyView notEnoughMoneyView)
        {
            _clients = clients ?? throw new ArgumentNullException(nameof(clients));
            _notEnoughMoneyView = notEnoughMoneyView ?? throw new ArgumentNullException(nameof(notEnoughMoneyView));
        }

        public async void OnClick()
        {
            foreach (var client in _clients)
            {
                if (client.CanBuyGoods() && client.ShoppingCart.Goods.Count() > 0)
                {
                    client.BuyGoods();
                }

                else
                {
                    _notEnoughMoneyView.Visualize(client.ShoppingCart.TotalPrice, client.Wallet.Money);
                    await Task.Delay(TimeSpan.FromSeconds(1.35f));
                }
            }
        }
    }
}