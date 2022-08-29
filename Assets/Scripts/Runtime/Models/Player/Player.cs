using System;
using Shooter.GameLogic;
using Shooter.Model;

namespace Shooter.Player
{
    public sealed class Player : IUpdateble
    {
        private readonly IWeaponInput _weaponInput;
        private readonly IWeapon _weapon;

        public Player(IWeaponInput weaponInput, IWeapon weapon)
        {
            _weaponInput = weaponInput ?? throw new ArgumentNullException(nameof(weaponInput));
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public Player SwitchWeapon(IWeapon weapon)
        {
            return new Player(_weaponInput, weapon);
        }

        public void Update(float deltaTime)
        {
            if (_weaponInput.IsPressingLeftMouseButton)
            {
                _weapon.Shoot();
            }
        }
    }
}