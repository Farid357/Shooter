using System;
using Shooter.GameLogic;
using Shooter.Root;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public sealed class DroppingWeaponSelector : IInventoryItemSelector<IDroppingWeapon>
    {
        private readonly IPlayerRoot _playerRoot;
        private readonly IFactory<IDroppingWeapon> _weaponsFactory;
        private readonly IBulletsView[] _bulletsViews;

        public DroppingWeaponSelector(IPlayerRoot playerRoot, IFactory<IDroppingWeapon> weaponsFactory, IBulletsView[] bulletsViews)
        {
            _playerRoot = playerRoot ?? throw new ArgumentNullException(nameof(playerRoot));
            _weaponsFactory = weaponsFactory ?? throw new ArgumentNullException(nameof(weaponsFactory));
            _bulletsViews = bulletsViews ?? throw new ArgumentNullException(nameof(bulletsViews));
        }

        public void Select(IDroppingWeapon weapon)
        {
            _bulletsViews.ForEach(view => view.Disable());

            if (_playerRoot.ComposedDroppingWeapon is not null && _playerRoot.ComposedDroppingWeapon.HasDropped)
            {
                var newWeapon  = _weaponsFactory.Create();
                _playerRoot.Compose(new BurstWeaponInput(), (dynamic)newWeapon);
            }
            
            else
            {
                _playerRoot.Compose(new BurstWeaponInput(), (dynamic) weapon);
            }
        }

        public void Unselect()
        {
            _playerRoot.Compose(new DummyWeaponInput(), new DummyGrenade());
        }
    }
}