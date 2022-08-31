using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Weapon : IWeapon
    {
        private readonly IFactory<IBullet> _bulletsFactory;
        private readonly IShotSound _sound;

        public Weapon(IFactory<IBullet> bulletsFactory, IShotSound sound)
        {
            _bulletsFactory = bulletsFactory ?? throw new ArgumentNullException(nameof(bulletsFactory));
            _sound = sound ?? throw new ArgumentNullException(nameof(sound));
        }

        public bool CanShoot => true;

        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException("Weapon can't shoot, but you're invoking method shoot!");
           
            _bulletsFactory.Create().Throw();
            _sound.Play();
        }
    }
}