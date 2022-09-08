using System;
using Shooter.SaveSystem;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Wallet : IWallet
    {
        private readonly IView<int> _view;
        private readonly StorageWithNameSaveObject<Wallet, int> _storage;
        private int _money;

        public Wallet(IView<int> view, IStorage storage)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _storage = new StorageWithNameSaveObject<Wallet, int>(storage);
        }

        public void Put(int money)
        {
            _money += money.TryThrowLessThanOrEqualsToZeroException();
            VisualizeAndSave(_money);
        }

        public bool CanTake(int money) => _money - money >= 0;

        public void Take(int money)
        {
            if (CanTake(money) == false)
                throw new InvalidOperationException(nameof(Take));

            _money -= money.TryThrowLessThanOrEqualsToZeroException();
            VisualizeAndSave(_money);
        }

        private void VisualizeAndSave(int money)
        {
            _storage.Save(money);
            _view.Visualize(money);
        }
    }
}

