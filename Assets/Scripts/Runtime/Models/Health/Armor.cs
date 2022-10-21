using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Armor : IHealth
    {
        private readonly IHealth _health;
        private readonly IArmorView _armorView;
        private int _protection;

        public Armor(IHealth health, IArmorView armorView, int protection)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _armorView = armorView ?? throw new ArgumentNullException(nameof(armorView));
            _protection = protection.TryThrowLessThanOrEqualsToZeroException();
            _armorView.Visualize(_protection);
        }

        public bool IsDied => _health.IsDied;
        
        public bool IsAlive => _health.IsAlive;

        public int StartValue => _health.StartValue;

        public int Value => _health.Value;
        
        public void TakeDamage(int damage)
        {
            damage.TryThrowLessThanOrEqualsToZeroException();
            
            if (_protection > 0 && _protection - damage >= 0)
            {
                _protection -= damage;
                damage = Math.Max(0, damage - _protection);
                
                if(damage == 0)
                    return;
            }
            
            _armorView.Visualize(_protection);
            _health.TakeDamage(damage);

        }

        public void Heal(int amount) => _health.Heal(amount);

        public bool CanHeal(int amount) => _health.CanHeal(amount);
        
    }
}