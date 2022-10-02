using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Weapon : IWeapon
    {
        private readonly IFactory<IBullet> _bulletsFactory;
        private readonly IBulletsView _bulletsView;

        public Weapon(IFactory<IBullet> bulletsFactory, IBulletsView bulletsView, int bullets)
        {
            _bulletsFactory = bulletsFactory ?? throw new ArgumentNullException(nameof(bulletsFactory));
            Bullets = bullets.TryThrowLessThanOrEqualsToZeroException();
            _bulletsView = bulletsView ?? throw new ArgumentNullException(nameof(bulletsView));
            StartBullets = Bullets;
            _bulletsView.Visualize(Bullets);
        }
        
        public int Bullets { get; private set; }
        
        public int StartBullets { get; }

        public bool CanShoot => Bullets > 0;
        
        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException("Weapon can't shoot, but you're invoking method shoot!");
           
            _bulletsFactory.Create().Throw();
            Bullets--;
            _bulletsView.Visualize(Bullets);
        }

        public void VisualizeBullets() => _bulletsView.Visualize(Bullets);

        public void AddBullets(int bullets)
        {
            Bullets += bullets.TryThrowLessThanOrEqualsToZeroException();
            _bulletsView.Visualize(Bullets);
        }
    }
}