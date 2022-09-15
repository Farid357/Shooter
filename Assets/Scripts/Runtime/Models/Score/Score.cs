using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Score : IScore
    {
        private readonly IView<int> _view;
        private readonly IScoreBestRecord _bestRecord;
        private int _amount;

        public Score(IView<int> view, IScoreBestRecord bestRecord)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _bestRecord = bestRecord ?? throw new ArgumentNullException(nameof(bestRecord));
        }
        
        public void Add(int amount)
        {
            _amount += amount.TryThrowLessThanOrEqualsToZeroException();
            _view.Visualize(_amount);
            
            if(_bestRecord.CanIncrease(_amount))
                _bestRecord.Increase(_amount);
        }
    }
}