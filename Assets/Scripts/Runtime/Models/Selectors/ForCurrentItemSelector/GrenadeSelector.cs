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
        private IGrenade _lastGrenade;

        public GrenadeSelector(IPlayerRoot playerRoot, IFactory<IGrenade> grenadesFactory)
        {
            _playerRoot = playerRoot ?? throw new ArgumentNullException(nameof(playerRoot));
            _grenadesFactory = grenadesFactory ?? throw new ArgumentNullException(nameof(grenadesFactory));
        }

        public void Select(IGrenade grenade)
        {
            if (_lastGrenade is not null && _lastGrenade.HasDropped)
            {
                var newGrenade  = _grenadesFactory.Create();
                _playerRoot.Compose(new StandartWeaponInput(), newGrenade);
                _lastGrenade = newGrenade;
            }
            
            else
            {
                _lastGrenade = grenade;
                _playerRoot.Compose(new StandartWeaponInput(), grenade);
            }
        }

        public void Unselect()
        {
            _playerRoot.Compose(new DummyWeaponInput(), new DummyWeapon());
        }
    }
}