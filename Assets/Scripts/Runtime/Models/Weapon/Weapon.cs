using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Weapon : IWeapon
    {
        private readonly IFactory<IBullet> _bulletsFactory;
        private readonly IShotView _shotView;

        public Weapon(IFactory<IBullet> bulletsFactory, IShotView shotView)
        {
            _bulletsFactory = bulletsFactory ?? throw new ArgumentNullException(nameof(bulletsFactory));
            _shotView = shotView ?? throw new ArgumentNullException(nameof(shotView));
        }

        public bool CanShoot => true;

        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException("Weapon can't shoot, but you're invoking method shoot!");
           
            _bulletsFactory.Create().Throw();
            _shotView.Visualize();
        }
    }
}