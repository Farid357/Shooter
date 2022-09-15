using System;
using Shooter.SaveSystem;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class ScoreBestRecord : IScoreBestRecord
    {
        private readonly IView<int> _view;
        private readonly StorageWithNameSaveObject<ScoreBestRecord, int> _storage;
        private int _amount;

        public ScoreBestRecord(IView<int> view, StorageWithNameSaveObject<ScoreBestRecord, int> storage)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _amount = _storage.HasSave() ? _storage.Load() : _amount;
        }
        
        public void Increase(int amount)
        {
            if (CanIncrease(amount) == false)
                throw new InvalidOperationException(nameof(Increase) + "can't increase!");

            _amount = amount.TryThrowLessThanOrEqualsToZeroException();
            _storage.Save(_amount);
            _view.Visualize(_amount);
        }

        public bool CanIncrease(int amount) => _amount < amount;
        
        
    }
}