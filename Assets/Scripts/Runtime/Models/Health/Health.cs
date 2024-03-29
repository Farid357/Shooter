﻿using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Health : IHealth
    {
        private readonly IHealthView _view;
        
        public Health(int amount, IHealthView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            Value = amount.TryThrowLessThanOrEqualsToZeroException();
            StartValue = Value;
            _view.Visualize(Value);
        }

        public int StartValue { get; }
        
        public int Value { get; private set; }
        
        public bool IsDied => Value == 0;
        
        public bool IsAlive => Value > 0;

        public void Heal(int amount)
        {
            if (CanHeal(amount) == false)
                throw new InvalidOperationException(nameof(Heal));
            
            Value += amount.TryThrowLessThanOrEqualsToZeroException();
            _view.Visualize(Value);
        }

        public bool CanHeal(int amount) => Value + amount <= StartValue && IsAlive;

        public void TakeDamage(int damage)
        {
            if (IsDied)
                throw new InvalidOperationException("Health is died!");

            damage.TryThrowLessThanOrEqualsToZeroException();
            Value = Math.Max(0, Value - damage);
            _view.Visualize(Value);
        }
    }
}