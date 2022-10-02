using NUnit.Framework;
using Shooter.Model;

namespace Shooter.Test
{
    [TestFixture]
    public sealed class PoisonHealthTest
    {
        [Test]
        public void TakesDamageCorrectly()
        {
            IHealth health = new PoisonHealth(new Health(10, new DummyHealthView()));
            health.TakeDamage(5);
            Assert.That(health.IsDied);
        }
    }
}