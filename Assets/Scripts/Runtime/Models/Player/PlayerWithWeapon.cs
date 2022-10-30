using System;
using Shooter.GameLogic;
using Shooter.Model;

namespace Shooter.Player
{
    public sealed class PlayerWithWeapon : IUpdateble
    {
        private readonly IWeaponInput _weaponInput;
        private readonly IShootingWeapon _weapon;

        public PlayerWithWeapon(IWeaponInput weaponInput, IShootingWeapon weapon)
        {
            _weaponInput = weaponInput ?? throw new ArgumentNullException(nameof(weaponInput));
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public void Update(float deltaTime)
        {
            if (_weaponInput.IsPressingLeftMouseButton && _weapon.CanShoot)
            {
                _weapon.Shoot();
            }
        }
    }
}