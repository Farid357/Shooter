using System;

namespace Shooter.Model
{
    public sealed class RandomRewardFactory : IRewardFactory
    {
        private readonly IAbility[] _abilities;
        private readonly IReward[] _otherRewards;
        
        public RandomRewardFactory(IAbility[] abilities, IReward[] otherRewards)
        {
            _abilities = abilities ?? throw new ArgumentNullException(nameof(abilities));
            _otherRewards = otherRewards ?? throw new ArgumentNullException(nameof(otherRewards));
        }
        
        public IReward Create()
        {
            var random = new Random().Next(0, 100);

            if (random < 25)
            {
                var randomIndex = new Random().Next(0, _abilities.Length);
                return new AbilityReward(_abilities[randomIndex]);
            }

            else
            {
                var randomIndex = new Random().Next(0, _otherRewards.Length);
                return _otherRewards[randomIndex];
            }
        }
    }
}