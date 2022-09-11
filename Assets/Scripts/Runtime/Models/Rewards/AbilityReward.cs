using System;

namespace Shooter.Model
{
    public sealed class AbilityReward : IReward
    {
        private readonly IAbility _ability;

        public AbilityReward(IAbility ability)
        {
            _ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }

        public void Apply() => _ability.Apply();
        
    }
}