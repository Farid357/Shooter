using System;

namespace Shooter.Model
{
    public sealed class DiedHealthsCounter : IDiedHealthsCounterReward, IReward
    {
        private readonly IView<int> _view;

        public DiedHealthsCounter(IView<int> view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }
        
        public int Amount { get; private set; }

        public void Apply()
        {
            Amount++;
            _view.Visualize(Amount);
        }
    }
}