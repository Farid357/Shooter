using System;
using Shooter.SaveSystem;

namespace Shooter.Model
{
    public sealed class SwitchingStateButtonAction<TUserStorage> : IButtonClickAction
    {
        private readonly ISwitchingStateButtonActionView _view;
        private readonly StorageWithNameSaveObject<TUserStorage, bool> _storage;
        
        public SwitchingStateButtonAction(IStorage storage, ISwitchingStateButtonActionView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _storage = new StorageWithNameSaveObject<TUserStorage, bool>(storage);
            State = _storage.HasSave() ? _storage.Load() : false;
            _view.Visualize(State);
        }
        
        public bool State { get; private set; }
        
        public void OnClick()
        {
            State = !State;
            _storage.Save(State);
            _view.Visualize(State);
        }

        public void Visualize() => _view.Visualize(State);
        
    }
}