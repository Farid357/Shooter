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

        public void Select((IWeapon, IWeaponInput) item)
        {
            var input = item.Item2;
            var weapon = item.Item1;
            _playerRoot.Compose(input, weapon);
            weapon.VisualizeBullets();
        }

        public void Unselect()
        {
            _playerRoot.Compose(new DummyWeaponInput(), new DummyWeapon());
        }
    }
}