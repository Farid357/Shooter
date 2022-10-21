using Shooter.Model;
using Shooter.SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class WalletRoot : SerializedMonoBehaviour, IWalletRoot
    {
        [SerializeField] private IView<int> _moneyView;
        [SerializeField] private IView<int> _diamondsView;

        private IWallet _coinsWallet;
        private IWallet _diamondsWallet;

        private IWallet Compose<TWalletType>(IView<int> countView)
        {
            return new Wallet<TWalletType>(countView, new BinaryStorage());
        }

        public IWallet CoinsWallet() => _coinsWallet ??= Compose<ICoins>(_moneyView);

        public IWallet DiamondsWallet() => _diamondsWallet ??= Compose<IDiamonds>(_diamondsView);
    }
}