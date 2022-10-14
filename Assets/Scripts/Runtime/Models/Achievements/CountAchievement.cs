using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class CountAchievement : IAchievement
    {
        private readonly IReadOnlyCounter _counter;
        private readonly IAchievement _achievement;
        private readonly int _needAmount;

        public CountAchievement(IReadOnlyCounter counter, IAchievement achievement, int needAmount)
        {
            _needAmount = needAmount.TryThrowLessThanOrEqualsToZeroException();
            _counter = counter ?? throw new ArgumentNullException(nameof(counter));
            _achievement = achievement ?? throw new ArgumentNullException(nameof(achievement));
        }

        public bool CanGet() => _counter.Amount <= _needAmount && _achievement.CanGet();

        public void Get()
        {
            if (CanGet() == false)
                throw new InvalidOperationException(nameof(Get));

            _achievement.Get();
        }
    }
}