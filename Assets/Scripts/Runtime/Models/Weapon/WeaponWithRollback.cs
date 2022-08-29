using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class WeaponWithRollback : IWeapon, IUpdateble
    {
        private readonly IWeapon _weapon;
        private readonly float _cooldown;
        private readonly int _startBullets;

        private int _bullets;
        private float _time;

        private bool NeedReload => _bullets == 0;

        public WeaponWithRollback(IWeapon weapon, int bullets, float cooldown)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _bullets = bullets.TryThrowLessThanOrEqualsToZeroException();
            _cooldown = cooldown.TryThrowLessOrEqualsToZeroException();
            _startBullets = bullets;
        }

        public bool CanShoot => _weapon.CanShoot && NeedReload == false;

        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException(nameof(Shoot));

            _weapon.Shoot();
            _bullets--;
        }

        public void Update(float deltaTime)
        {
            if (NeedReload)
            {
                TryReload(deltaTime);
            }
        }

        private void TryReload(float deltaTime)
        {
            _time += deltaTime;
            if (_time >= _cooldown)
            {
                _bullets = _startBullets;
            }
        }
    }
}