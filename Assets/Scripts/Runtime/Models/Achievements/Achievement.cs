using System;

namespace Shooter.Model
{
    public sealed class Achievement : IAchievement
    {
        private readonly IAchievementView _achievementView;
        private readonly IReward _gettingReward;
        private bool _canGet = true;

        public Achievement(IAchievementView achievementView, IReward gettingReward)
        {
            _achievementView = achievementView ?? throw new ArgumentNullException(nameof(achievementView));
            _gettingReward = gettingReward ?? throw new ArgumentNullException(nameof(gettingReward));
        }

        public bool CanGet() => _canGet;

        public void Get()
        {
            if (CanGet() == false)
                throw new InvalidOperationException("Already got!");

            _achievementView.VisualizeGetting();
            _gettingReward.Apply();
            _canGet = false;
        }
    }
}