using System;
using Shooter.Model;

namespace Shooter.GameLogic
{
    public sealed class DualWeaponFactory : IWeaponFactory
    {
        private readonly IWeaponFactory _firstWeaponFactory;
        private readonly IWeaponFactory _secondWeaponFactory;

        public DualWeaponFactory(IWeaponFactory firstWeaponFactory, IWeaponFactory secondWeaponFactory)
        {
            _firstWeaponFactory = firstWeaponFactory ?? throw new ArgumentNullException(nameof(firstWeaponFactory));
            _secondWeaponFactory = secondWeaponFactory ?? throw new ArgumentNullException(nameof(secondWeaponFactory));
        }

        public IWeapon Create()
        {
            return new DualWeapon(_firstWeaponFactory.Create(), _secondWeaponFactory.Create());
        }
    }
}