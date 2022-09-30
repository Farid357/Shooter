using System;

namespace Shooter.Model
{
    public sealed class DiedHealthsCounterReward : IReward
    {
        private readonly IView<int> _view;
        private int _count;
        
        public DiedHealthsCounterReward(IView<int> view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Apply()
        {
            _count++;
            _view.Visualize(_count);
        }
    }
}