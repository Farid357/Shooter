using NUnit.Framework;
using Shooter.Model;

namespace Shooter.Test
{
    public sealed class HealthTest
    {
        [Test]
        public void AddHealthCorrectly()
        {
            var health = new Health(100, new DummyHealthView());
            health.TakeDamage(5);
            Assert.That(health.Value == 95);
        }

        [Test]
        public void HealthVisualizeValue()
        {
            var view = new DummyHealthView();
            var health = new Health(100, view);
            health.TakeDamage(5);
            Assert.That(view.HasVusualized);
        }

        [Test]
        public void HealthBoolIsAliveWorksCorrectly()
        {
            var health = new Health(100, new DummyHealthView());
            health.TakeDamage(100);
            Assert.That(health.IsAlive == false);
        }
    }
}