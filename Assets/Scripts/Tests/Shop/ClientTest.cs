using System.Linq;
using NUnit.Framework;
using Shooter.Model;
using Shooter.Shop;

namespace Shooter.Test
{
    [TestFixture]
    public sealed class ClientTest
    {
        private IClient _client;
        private IWallet _wallet;
        private IShoppingCart _shoppingCart;
        
        [SetUp]
        public void SetUp()
        {
            _shoppingCart = new ShoppingCart(new DummyShoppingCartView());
            _wallet = new Wallet<IAbility>(new DummyCountView(), new DummyStorage());
            _client = new Client(_wallet, _shoppingCart);
        }
        
        [Test]
        public void BuysCorrectly()
        {
            _wallet.Put(600);
            _shoppingCart.Add(new DummyGood());
            _client.BuyGoods();
            Assert.That(_shoppingCart.Goods.Count() == 0 && _wallet.Money == 100);
        }
        
        [Test]
        public void CanBuyItemsWorkCorrectly()
        {
            _wallet.Put(500);
            _shoppingCart.Add(new DummyGood());
            _client.BuyGoods();
            _shoppingCart.Add(new DummyGood());
            Assert.That(_client.CanBuyGoods() == false);
        }
    }
}