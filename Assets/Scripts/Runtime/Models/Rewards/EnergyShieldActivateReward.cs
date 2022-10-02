using System;

namespace Shooter.Model
{
    public sealed class EnergyShieldActivateReward : IReward
    {
        private readonly IEnergyShield _energyShield;

        public EnergyShieldActivateReward(IEnergyShield energyShield)
        {
            _energyShield = energyShield ?? throw new ArgumentNullException(nameof(energyShield));
        }

        public void Apply() => _energyShield.Activate();
        
    }
}