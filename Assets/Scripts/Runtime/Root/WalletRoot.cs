using Shooter.Model;
using Shooter.SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class WalletRoot : SerializedMonoBehaviour, IWalletRoot
    {
        [SerializeField] private IView<int> _moneyView;
        private IWallet _wallet;

        private IWallet Compose()
        {
            return new Wallet(_moneyView, new BinaryStorage());
        }

        public IWallet Wallet()
        {
            _wallet ??= Compose();
            return _wallet;
        }
    }
}