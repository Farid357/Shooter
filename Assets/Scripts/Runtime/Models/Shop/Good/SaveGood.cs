using System;
using Shooter.SaveSystem;

namespace Shooter.Shop
{
    public sealed class SaveGood<TUserStorage, TStoreValue> : IGood
    {
        private readonly IGood _good;
        private readonly TStoreValue _storeValue;
        private readonly StorageWithNameSaveObject<TUserStorage, TStoreValue> _storage;

        public SaveGood(IGood good, TStoreValue storeValue, IStorage storage)
        {
            _good = good ?? throw new ArgumentNullException(nameof(good));
            _storeValue = storeValue ?? throw new ArgumentNullException(nameof(storeValue));
            _storage = new StorageWithNameSaveObject<TUserStorage, TStoreValue>(storage);
        }

        public IGoodData Data => _good.Data;
        
        public void Use()
        {
            _storage.Save(_storeValue);
            _good.Use();
        }
    }
}