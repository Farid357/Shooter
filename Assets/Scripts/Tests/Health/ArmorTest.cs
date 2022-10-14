using NUnit.Framework;
using Shooter.GameLogic;
using Shooter.Model;

namespace Shooter.Test
{
    [TestFixture]
    public sealed class ArmorTest
    {
        [Test]
        public void TakesDamageCorrectly()
        {
            IHealth shield = new Armor(new Health(10, new DummyHealthView()), new DummyArmorView(), 100);
            shield.TakeDamage(5);
            Assert.That(shield.Value == 10);
        }
    }
}