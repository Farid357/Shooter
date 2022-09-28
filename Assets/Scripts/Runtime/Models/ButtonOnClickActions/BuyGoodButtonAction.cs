using System;
using JetBrains.Annotations;
using Shooter.Shop;

namespace Shooter.Model
{
    public sealed class BuyGoodButtonAction : IButtonClickAction
    {
        private readonly IClient _client;

        public BuyGoodButtonAction(IClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public void OnClick() => _client.BuyItems();
        
    }
}