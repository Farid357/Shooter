using System;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Root;

namespace Shooter.GameLogic.Inventory
{
    public sealed class WeaponSelector : IItemSelector<(IWeapon, IWeaponInput)>
    {
        private readonly IPlayerRoot _playerRoot;

        public WeaponSelector(IPlayerRoot playerRoot)
        {
            _playerRoot = playerRoot ?? throw new ArgumentNullException(nameof(playerRoot));
        }

        public void Select((IWeapon, IWeaponInput) item)
        {
            var (weapon, weaponInput) = item;
            _playerRoot.Compose(weapon, weaponInput);
        }
    }
}