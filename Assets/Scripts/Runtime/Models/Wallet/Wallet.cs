using System;
using Shooter.SaveSystem;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Wallet : IWallet
    {
        private readonly IView<int> _view;
        private readonly StorageWithNameSaveObject<Wallet, int> _storage;

        public Wallet(IView<int> view, IStorage storage)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _storage = new StorageWithNameSaveObject<Wallet, int>(storage);
        }
        
        public int Money { get; private set; }

        public void Put(int money)
        {
            Money += money.TryThrowLessThanOrEqualsToZeroException();
            VisualizeAndSave(Money);
        }

        public bool CanTake(int money) => Money - money >= 0;

        public void Take(int money)
        {
            if (CanTake(money) == false)
                throw new InvalidOperationException(nameof(Take));

            Money -= money.TryThrowLessThanOrEqualsToZeroException();
            VisualizeAndSave(Money);
        }

        private void VisualizeAndSave(int money)
        {
            _storage.Save(money);
            _view.Visualize(money);
        }
    }
}

