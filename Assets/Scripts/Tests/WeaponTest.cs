using NUnit.Framework;
using Shooter.Model;

namespace Shooter.Test
{
    public sealed class WeaponTest
    {
        [Test]
        public void WeaponWithRollbackCalculateBulletsCorrectly()
        {
            var weapon = new Weapon(new DummyBulletsFactory(), new DummyBulletsView(), 10);
            weapon.Shoot();
            Assert.That(weapon.Bullets == 9);
        }

        [Test]
        public void WeaponVisualizeBulletsCorrectly()
        {
            var view = new DummyBulletsView();
            var weapon = new Weapon(new DummyBulletsFactory(), view, 10);
            weapon.Shoot();
            Assert.That(view.Bullets == 9);
        }
    }
}