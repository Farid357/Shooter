using Shooter.Model;

namespace Shooter.Shop
{
    public interface IClient
    {
        IReadOnlyShoppingCart ShoppingCart { get; }
        
        IWallet Wallet { get; }
        
        bool CanBuyGoods();
        
        void BuyGoods();
    }
}