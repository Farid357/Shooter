using System;

namespace Shooter.Model
{
    public sealed class HealthPotion : IShootingWeapon
    {
        private readonly IHealth _health;
        private readonly IPotionView _view;
        private readonly int healAmount;

        public HealthPotion(IHealth health, IPotionView view)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            healAmount = new Random().Next(10, 50);
        }

        public bool CanShoot { get; private set; } = true;
        
        public async void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException(nameof(Shoot));
            
            CanShoot = false;
            await _view.VisualizeShot();
            
            if (_health.CanHeal(healAmount))
                _health.Heal(healAmount);
            
        }
    }
}