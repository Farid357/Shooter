using System;
using Shooter.GameLogic;
using Shooter.Model;

namespace Shooter.Player
{
    public sealed class Player : IUpdateble
    {
        private readonly IWeaponInput _weaponInput;
        private readonly IShootingWeapon _weapon;

        public Player(IWeaponInput weaponInput, IShootingWeapon weapon)
        {
            _weaponInput = weaponInput ?? throw new ArgumentNullException(nameof(weaponInput));
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public void Update(float deltaTime)
        {
            if (_weaponInput.IsPressingLeftMouseButton)
            {
                if (_weapon.CanShoot)
                    _weapon.Shoot();
            }
        }
    }
}