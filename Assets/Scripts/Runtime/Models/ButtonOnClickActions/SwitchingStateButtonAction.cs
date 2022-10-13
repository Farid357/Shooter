using System;
using Shooter.SaveSystem;

namespace Shooter.Model
{
    public sealed class SwitchingStateButtonAction<TUserStorage> : IButtonClickAction
    {
        private readonly ISwitchingStateButtonActionView _view;
        private readonly StorageWithNameSaveObject<TUserStorage, bool> _storage;
        private bool _state;
        
        public SwitchingStateButtonAction(IStorage storage, ISwitchingStateButtonActionView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _storage = new StorageWithNameSaveObject<TUserStorage, bool>(storage);
            _state = _storage.HasSave() ? _storage.Load() : false;
            _view.Visualize(_state);
        }
        
        public void OnClick()
        {
            _state = !_state;
            _storage.Save(_state);
            _view.Visualize(_state);
        }
    }
}