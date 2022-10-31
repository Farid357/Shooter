using System;
using Shooter.Model;
using Shooter.Tools;
using ArgumentNullException = System.ArgumentNullException;

namespace Shooter.GameLogic
{
    public sealed class WeaponFactoryWithShootWaiting : IWeaponFactory
    {
        private readonly IFactory<IBullet> _bulletsFactory;
        private readonly WeaponData _data;

        public WeaponFactoryWithShootWaiting(IFactory<IBullet> bulletsFactory, WeaponData data)
        {
            _bulletsFactory = bulletsFactory ?? throw new ArgumentNullException(nameof(bulletsFactory));
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }
        
        public IWeapon Create()
        {
            var weapon = new Weapon(_bulletsFactory, _data.BulletsView, _data.Bullets);
            var weaponWithShotWaiting = new WeaponWithShotWaiting(weapon, _data.WaitSeconds);
            return weaponWithShotWaiting;
        }
    }
}