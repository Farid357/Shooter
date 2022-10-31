using System;
using Shooter.GameLogic;
using Shooter.Root;

namespace Shooter.Model.Inventory
{
    public sealed class WeaponSelector : IInventoryItemSelector<(IWeapon, IWeaponInput)>
    {
        private readonly IPlayerRoot _playerRoot;
        private readonly IBulletsView _secondWeaponBulletsView;

        public WeaponSelector(IPlayerRoot playerRoot, IBulletsView secondWeaponBulletsView)
        {
            _playerRoot = playerRoot ?? throw new ArgumentNullException(nameof(playerRoot));
            _secondWeaponBulletsView = secondWeaponBulletsView ?? throw new ArgumentNullException(nameof(secondWeaponBulletsView));
        }

        public void Select((IWeapon, IWeaponInput) item)
        {
            var input = item.Item2;
            var weapon = item.Item1;
            _playerRoot.Compose(input, weapon);
            weapon.VisualizeBullets();
            
            if(weapon is not DualWeapon)
                _secondWeaponBulletsView.Disable();
        }

        public void Unselect()
        {
            _playerRoot.Compose(new DummyWeaponInput(), new DummyWeapon());
        }
    }
}