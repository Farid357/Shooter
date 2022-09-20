using NUnit.Framework;
using Shooter.Model;

namespace Shooter.Test
{
    [TestFixture]
    public sealed class ShieldTest
    {
        [Test]
        public void TakesDamageCorrectly()
        {
            IHealth shield = new HealthShield(new Health(10, new DummyHealthView()), 100);
            shield.TakeDamage(5);
            Assert.That(shield.Value == 10);
        }
    }
}