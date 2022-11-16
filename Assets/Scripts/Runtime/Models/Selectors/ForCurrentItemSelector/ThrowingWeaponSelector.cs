using System;
using Shooter.GameLogic;
using Shooter.Root;
using Shooter.Tools;

namespace Shooter.Model.Inventory
{
    public sealed class ThrowingWeaponSelector : IInventoryItemSelector<IThrowingWeapon>
    {
        private readonly IPlayerRoot _playerRoot;
        private readonly IBulletsView[] _bulletsViews;

        public ThrowingWeaponSelector(IPlayerRoot playerRoot, IBulletsView[] bulletsViews)
        {
            _playerRoot = playerRoot ?? throw new ArgumentNullException(nameof(playerRoot));
            _bulletsViews = bulletsViews ?? throw new ArgumentNullException(nameof(bulletsViews));
        }

        public void Select(IThrowingWeapon weapon)
        {
            _bulletsViews.ForEach(view => view.Disable());
            _playerRoot.Compose(new BurstWeaponInput(), (dynamic)weapon);
        }

        public void Unselect()
        {
            _playerRoot.Compose(new DummyWeaponInput(), new DummyThrowingWeapon());
        }
    }
}