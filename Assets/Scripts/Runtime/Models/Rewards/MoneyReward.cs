using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class MoneyReward : IReward
    {
        private readonly IWallet _wallet;
        private readonly int _money;

        public MoneyReward(IWallet wallet, int money)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _money = money.TryThrowLessThanOrEqualsToZeroException();
        }

        public void Apply() => _wallet.Put(_money);
        
    }
}