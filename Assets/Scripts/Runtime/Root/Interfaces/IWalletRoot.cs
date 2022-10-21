using Shooter.Model;

namespace Shooter.Root
{
    public interface IWalletRoot
    {
        IWallet CoinsWallet();

        IWallet DiamondsWallet();
    }
}