using System;
using Shooter.GameLogic;
using Shooter.Root;

namespace Shooter.Model.Inventory
{
    public sealed class GrenadeSelector : IInventoryItemSelector<IGrenade>
    {
        private readonly IPlayerRoot _playerRoot;

        public GrenadeSelector(IPlayerRoot playerRoot)
        {
            _playerRoot = playerRoot ?? throw new ArgumentNullException(nameof(playerRoot));
        }
        
        public void Select(IGrenade grenade)
        {
            _playerRoot.Compose(new StandartWeaponInput(), grenade);
        }

        public void Unselect()
        {
            _playerRoot.Compose(new DummyWeaponInput(), new DummyWeapon());
        }
    }
}