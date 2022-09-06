using System;
using Shooter.GameLogic;
using Shooter.Model;

namespace Shooter.Player
{
    public sealed class Player : IPlayer, IUpdateble
    {
        private readonly IWeaponInput _weaponInput;
        private IWeapon _weapon;

        public Player(IWeaponInput weaponInput, IWeapon weapon)
        {
            _weaponInput = weaponInput ?? throw new ArgumentNullException(nameof(weaponInput));
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public void SwitchWeapon(IWeapon weapon)
        {
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