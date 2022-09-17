using System;
using Shooter.GameLogic;
using Shooter.Root;

namespace Shooter.Model.Inventory
{
    public sealed class WeaponSelector : IInventoryItemSelector<(IWeapon, IWeaponInput)>
    {
        private readonly IPlayerRoot _playerRoot;

        public WeaponSelector(IPlayerRoot playerRoot)
        {
            _playerRoot = playerRoot ?? throw new ArgumentNullException(nameof(playerRoot));
        }

        public void Select((IWeapon, IWeaponInput) grenade)
        {
            var input = grenade.Item2;
            var weapon = grenade.Item1;
            _playerRoot.Compose(weapon, input);
            weapon.VisualizeBullets();
        }

        public void Unselect()
        {
            _playerRoot.Compose(new DummyWeapon(), new DummyWeaponInput());
        }
    }
}