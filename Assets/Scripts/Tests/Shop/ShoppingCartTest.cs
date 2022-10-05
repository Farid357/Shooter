using System.Linq;
using NUnit.Framework;
using Shooter.Shop;

namespace Shooter.Test
{
    [TestFixture]
    public sealed class ShoppingCartTest
    {
        private DummyShoppingCartView _shoppingCartView;
        private IShoppingCart _shoppingCart;
        
        [SetUp]
        public void SetUp()
        {
            _shoppingCartView = new DummyShoppingCartView();
            _shoppingCart = new ShoppingCart(_shoppingCartView);
        }
        
        [Test]
        public void AddsCorrectly()
        {
            _shoppingCart.Add(new DummyGood());
            Assert.That(_shoppingCart.Goods.Count() == 1);
        }
        
        [Test]
        public void VisualizeTotalPriceCorrectly()
        {
            _shoppingCart.Add(new DummyGood());
            _shoppingCart.Add(new DummyGood());
            Assert.That(_shoppingCartView.TotalPrice == 1000);
        }
    }
}