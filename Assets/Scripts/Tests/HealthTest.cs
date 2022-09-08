using System;
using NUnit.Framework;
using Shooter.Model;

namespace Shooter.Test
{
    public sealed class HealthTest
    {
        [Test]
        public void AddHealthCorrectly()
        {
            IHealth health = new Health(100, new DummyHealthView());
            health.TakeDamage(5);
            Assert.That(health.Value == 95);
        }

        [Test]
        public void HealthVisualizeValue()
        {
            var view = new DummyHealthView();
            IHealth health = new Health(100, view);
            health.TakeDamage(5);
            Assert.That(view.HasVusualized);
        }

        [Test]
        public void HealthBoolIsAliveWorksCorrectly()
        {
            IHealth health = new Health(100, new DummyHealthView());
            health.TakeDamage(100);
            Assert.That(health.IsAlive == false);
        }

        [Test]
        public void HealCorrectly()
        {
            IHealth health = new Health(100, new DummyHealthView());
            health.TakeDamage(5);
            health.Heal(5);
            Assert.That(health.Value == 100);
        }

        [Test]
        public void InvalidHealThrowsException()
        {
            IHealth health = new Health(100, new DummyHealthView());
            Assert.Throws<InvalidOperationException>((() => health.Heal(100)));
        }
    }
}