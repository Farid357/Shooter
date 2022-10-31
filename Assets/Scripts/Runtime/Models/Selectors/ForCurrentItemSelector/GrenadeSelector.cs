using System;
using Shooter.GameLogic;
using Shooter.Root;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public sealed class GrenadeSelector : IInventoryItemSelector<IGrenade>
    {
        private readonly IPlayerRoot _playerRoot;
        private readonly IFactory<IGrenade> _grenadesFactory;
        private readonly IBulletsView _bulletsView;
        private IGrenade _lastGrenade;

        public GrenadeSelector(IPlayerRoot playerRoot, IFactory<IGrenade> grenadesFactory, IBulletsView bulletsView)
        {
            _playerRoot = playerRoot ?? throw new ArgumentNullException(nameof(playerRoot));
            _grenadesFactory = grenadesFactory ?? throw new ArgumentNullException(nameof(grenadesFactory));
            _bulletsView = bulletsView ?? throw new ArgumentNullException(nameof(bulletsView));
        }

        public void Select(IGrenade grenade)
        {
            _bulletsView.Disable();
            
            if (_lastGrenade is not null && _lastGrenade.HasDropped)
            {
                var newGrenade  = _grenadesFactory.Create();
                _playerRoot.Compose(new BurstWeaponInput(), newGrenade);
                _lastGrenade = newGrenade;
            }
            
            else
            {
                _lastGrenade = grenade;
                _playerRoot.Compose(new BurstWeaponInput(), grenade);
            }

            _lastGrenade?.ItemView.Show();
        }

        public void Unselect()
        {
            _playerRoot.Compose(new DummyWeaponInput(), new DummyWeapon());
        }
    }
}