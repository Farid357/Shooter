using System;
using Shooter.SaveSystem;

namespace Shooter.Shop
{
    public sealed class SaveHasActivatedGood<TUserStorage> : IGood
    {
        private readonly StorageWithNameSaveObject<TUserStorage, bool> _storage;
        private readonly IGood _good;

        public SaveHasActivatedGood(IGood good, IStorage storage)
        {
            _good = good ?? throw new ArgumentNullException(nameof(good));
            _storage = new StorageWithNameSaveObject<TUserStorage, bool>(storage);
        }

        public IGoodData Data => _good.Data;
        
        public void Use()
        {
            _good.Use();
            _storage.Save(true);
        }
    }
}