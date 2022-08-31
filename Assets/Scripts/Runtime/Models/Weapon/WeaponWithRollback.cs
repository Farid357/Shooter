using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class WeaponWithRollback : IWeapon
    {
        private readonly IWeapon _weapon;
        private readonly IView<int> _view;
        private int _bullets;

        public WeaponWithRollback(IWeapon weapon, int bullets, IView<int> view)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _bullets = bullets.TryThrowLessThanOrEqualsToZeroException();
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _view.Visualize(_bullets);
        }

        private bool NeedReload => _bullets == 0;
        
        public bool CanShoot => _weapon.CanShoot && NeedReload == false;

        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException(nameof(Shoot));

            _weapon.Shoot();
            _bullets--;
            _view.Visualize(_bullets);
        }
    }
}