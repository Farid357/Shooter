using System;
using Shooter.Model.Inventory;

namespace Shooter.Model
{
    public sealed class NegativeHealthPotion : IPotion
    {
        private readonly IHealth _health;
        private readonly IPotionView _view;
        private readonly int _damage;
        
        public NegativeHealthPotion(IHealth health, IPotionView view, IInventoryItemGameObjectView itemView)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _damage = new Random().Next(5, 20);
            ItemView = itemView ?? throw new ArgumentNullException(nameof(itemView));
        }

        public bool CanShoot { get; private set; } = true;
        
        public IInventoryItemGameObjectView ItemView { get; }

        public async void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException(nameof(Shoot));

            CanShoot = false;
            await _view.VisualizeShot();
            
            if(_health.IsAlive)
                _health.TakeDamage(_damage);
        }

    }
}