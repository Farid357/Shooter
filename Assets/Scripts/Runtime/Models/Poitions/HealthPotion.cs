using System;
using Shooter.Model.Inventory;

namespace Shooter.Model
{
    public sealed class HealthPotion : IPotion
    {
        private readonly IHealth _health;
        private readonly IPotionView _view;
        private readonly int healAmount;

        public HealthPotion(IHealth health, IPotionView view, IInventoryItemGameObjectView itemView)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            ItemView = itemView ?? throw new ArgumentNullException(nameof(itemView));
            healAmount = new Random().Next(10, 50);
        }

        public bool CanShoot { get; private set; } = true;

        public bool HasDropped { get; private set; }
        
        public IInventoryItemGameObjectView ItemView { get; }

        public async void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException(nameof(Shoot));

            HasDropped = true;
            CanShoot = false;
            await _view.VisualizeShot();
            
            if (_health.CanHeal(healAmount))
                _health.Heal(healAmount);
            
        }

    }
}