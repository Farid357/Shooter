using System;

namespace Shooter.Model
{
    public sealed class Weapon : IWeapon
    {
        private readonly IBulletsFactory _bulletsFactory;

        public Weapon(IBulletsFactory bulletsFactory)
        {
            _bulletsFactory = bulletsFactory ?? throw new ArgumentNullException(nameof(bulletsFactory));
        }

        public bool CanShoot => true;
        
        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException("Weapon can't shoot, but you're invoking method shoot!");
            _bulletsFactory.Create().Throw();
        }
        
    }
}