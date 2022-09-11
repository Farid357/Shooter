using System;
using Shooter.Model;
using Shooter.Tools;

namespace Shooter.GameLogic
{
    public sealed class WeaponFactoryWithShootWaitingAndRollback : IWeaponFactory
    {
        private readonly IFactory<IBullet> _bulletsFactory;
        private readonly WeaponData _data;

        public WeaponFactoryWithShootWaitingAndRollback(IFactory<IBullet> bulletsFactory, WeaponData data)
        {
            _bulletsFactory = bulletsFactory ?? throw new ArgumentNullException(nameof(bulletsFactory));
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public IWeapon Create()
        {
            var weapon = new Weapon(_bulletsFactory, _data.ShotView, _data.BulletsView, _data.Bullets);
            var weaponWithShotWaiting = new WeaponWithShotWaiting(weapon, _data.WaitSeconds);
            return weaponWithShotWaiting;
        }
    }
}