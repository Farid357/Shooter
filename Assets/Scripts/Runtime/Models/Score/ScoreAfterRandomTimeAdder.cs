using System;
using Cysharp.Threading.Tasks;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class ScoreAfterRandomTimeAdder
    {
        private readonly IScore _score;
        private readonly int _scoreAmount;

        public ScoreAfterRandomTimeAdder(IScore score, int scoreAmount)
        {
            _score = score ?? throw new ArgumentNullException(nameof(score));
            _scoreAmount = scoreAmount.TryThrowLessThanOrEqualsToZeroException();
        }

        public async UniTaskVoid TryAddLoop()
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(new Random().Next(1, 5)));
                _score.Add(_scoreAmount);
            }
        }
    }
}