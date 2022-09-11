using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class MoneyReward : IReward
    {
        private readonly IWallet _wallet;
        private readonly int _addMoney;

        public MoneyReward(IWallet wallet, int addMoney)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _addMoney = addMoney.TryThrowLessThanOrEqualsToZeroException();
        }

        public void Apply() => _wallet.Put(_addMoney);
        
    }
}