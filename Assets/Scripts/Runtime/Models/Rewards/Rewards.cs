using System;
using System.Collections.Generic;
using Sirenix.Utilities;

namespace Shooter.Model
{
    public sealed class Rewards : IReward
    {
        private readonly IEnumerable<IReward> _rewards;

        public Rewards(IEnumerable<IReward> rewards)
        {
            _rewards = rewards ?? throw new ArgumentNullException(nameof(rewards));
        }

        public void Apply()
        {
            _rewards.ForEach(reward => reward.Apply());
        }
    }
}