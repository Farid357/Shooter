using System;
using Shooter.SaveSystem;

namespace Shooter.Model
{
    public sealed class SaveAndChangeBoolButtonAction<TForStorage> : IButtonClickAction
    {
        private readonly ISaveAndChangeBoolButtonActionView _view;
        private readonly StorageWithNameSaveObject<TForStorage, bool> _storage;
        private bool _state;
        
        public SaveAndChangeBoolButtonAction(IStorage storage, ISaveAndChangeBoolButtonActionView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _storage = new StorageWithNameSaveObject<TForStorage, bool>(storage);
            _state = _storage.Load();
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