using System;
using System.Collections.Generic;
using System.Linq;

namespace Shooter.Model
{
    public sealed class RandomRewardFactory : IRewardFactory
    {
        private readonly IEnumerable<IAbility> _abilities;
        private readonly IReward[] _otherRewards;
        
        public RandomRewardFactory(IEnumerable<IAbility> abilities, IReward[] otherRewards)
        {
            _abilities = abilities ?? throw new ArgumentNullException(nameof(abilities));
            _otherRewards = otherRewards ?? throw new ArgumentNullException(nameof(otherRewards));
        }
        
        public IReward Create()
        {
            var random = new Random().Next(0, 100);

            if (random < 25)
            {
                var randomIndex = new Random().Next(0, _abilities.Count());
                return new AbilityReward(_abilities.ElementAt(randomIndex));
            }

            else
            {
                var randomIndex = new Random().Next(0, _otherRewards.Length);
                return _otherRewards[randomIndex];
            }
        }
    }
}