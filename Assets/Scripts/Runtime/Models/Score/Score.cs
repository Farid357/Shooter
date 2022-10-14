using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Score : IScore
    {
        private readonly IView<int> _view;
        private readonly IScoreBestRecord _bestRecord;

        public Score(IView<int> view, IScoreBestRecord bestRecord)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _bestRecord = bestRecord ?? throw new ArgumentNullException(nameof(bestRecord));
        }
        
        public int Amount { get; private set; }
        
        public void Add(int amount)
        {
            Amount += amount.TryThrowLessThanOrEqualsToZeroException();
            _view.Visualize(Amount);
            
            if(_bestRecord.CanIncrease(Amount))
                _bestRecord.Increase(Amount);
        }
    }
}