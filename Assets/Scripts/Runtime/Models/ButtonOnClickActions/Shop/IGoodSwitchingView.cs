using Shooter.Shop;

namespace Shooter.Model
{
    public interface IGoodSwitchingView
    {
        void Switch(IGood good, WalletType forPayment);
    }
}