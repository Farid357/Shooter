using System;
using Shooter.SaveSystem;

namespace Shooter.Shop
{
    public sealed class SaveWeaponCollectionGood<TSaveEnum> : IGood where TSaveEnum : Enum
    {
        private readonly IGood _good;
        private readonly ICollectionStorage<TSaveEnum> _weaponStorage;
        private readonly TSaveEnum _weapon;

        public SaveWeaponCollectionGood(IGood good, TSaveEnum weapon, ICollectionStorage<TSaveEnum> weaponStorage)
        {
            _good = good ?? throw new ArgumentNullException(nameof(good));
            _weapon = weapon;
            _weaponStorage = weaponStorage ?? throw new ArgumentNullException(nameof(weaponStorage));
        }

        public IGoodData Data => _good.Data;
        
        public void Use()
        {
            _weaponStorage.Save(WeaponsKey.Value, _weapon);
            _good.Use();
        }
    }
}