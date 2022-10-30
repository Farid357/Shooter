using System;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Model.Inventory;

namespace Shooter.Root
{
    public sealed class PlayerWithGrenade : IUpdateble
    {
        private readonly IGrenade _grenade;
        private readonly IWeaponInput _input;
        private readonly IInventory<IGrenade> _inventory;

        public PlayerWithGrenade(IGrenade grenade, IWeaponInput input, IInventory<IGrenade> inventory)
        {
            _grenade = grenade ?? throw new ArgumentNullException(nameof(grenade));
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public void Update(float deltaTime)
        {
            if (_input.IsPressingLeftMouseButton && _grenade.CanShoot)
            {
                _grenade.Shoot();
                _inventory.Drop(_grenade);
            }
        }
    }
}