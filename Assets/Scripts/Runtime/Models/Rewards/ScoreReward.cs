using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class ScoreReward : IReward
    {
        private readonly IScore _score;
        private readonly int _addAmount;

        public ScoreReward(IScore score, int addAmount)
        {
            _score = score ?? throw new ArgumentNullException(nameof(score));
            _addAmount = addAmount.TryThrowLessThanOrEqualsToZeroException();
        }

        public void Apply() => _score.Add(_addAmount);
        
    }
}