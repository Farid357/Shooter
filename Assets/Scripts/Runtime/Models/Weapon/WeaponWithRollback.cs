using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class WeaponWithRollback : IWeapon, IWeaponWithRollback
    {
        private readonly IWeapon _weapon;
        private readonly IBulletsView _view;

        public WeaponWithRollback(IWeapon weapon, IBulletsView view, int bullets)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            Bullets = bullets.TryThrowLessThanOrEqualsToZeroException();
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _view.Visualize(Bullets);
        }

        private bool NeedReload => Bullets == 0;
        
        public int Bullets { get; private set; }
        
        public bool CanShoot => _weapon.CanShoot && NeedReload == false;

        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException(nameof(Shoot));

            _weapon.Shoot();
            Bullets--;
            _view.Visualize(Bullets);
        }

        public void AddBullets(int bullets)
        {
            Bullets += bullets.TryThrowLessThanOrEqualsToZeroException();
        }
    }
}