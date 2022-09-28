using System;
using Shooter.SaveSystem;

namespace Shooter.Shop
{
    public sealed class WeaponGood : IGood
    {
        private readonly IGood _good;
        private readonly ICollectionStorage<WeaponType> _weaponStorage;
        private readonly WeaponType _weapon;

        public WeaponGood(IGood good, WeaponType weapon, ICollectionStorage<WeaponType> weaponStorage)
        {
            _good = good ?? throw new ArgumentNullException(nameof(good));
            _weapon = weapon;
            _weaponStorage = weaponStorage ?? throw new ArgumentNullException(nameof(weaponStorage));
        }

        public GoodData Data => _good.Data;
        
        public void Use()
        {
            _weaponStorage.Save(WeaponsKey.Value, _weapon);
            _good.Use();
        }
    }
}