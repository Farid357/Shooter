using System;
using Shooter.Model.Inventory;

namespace Shooter.Model
{
    public sealed class SpeedPotion : IPotion
    {
        private readonly ICharacterMovement _movement;
        private readonly IPotionView _view;
        private readonly float _increaseSpeed;
        private const float IncreaseSpeedSeconds = 2f;
        
        public SpeedPotion(ICharacterMovement movement, IPotionView view, IInventoryItemGameObjectView itemView)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            ItemView = itemView ?? throw new ArgumentNullException(nameof(itemView));
            var randomNumber = (float) new Random().NextDouble() * 2;
            _increaseSpeed = randomNumber < 1.2f ? 1.2f : randomNumber;
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
            _movement.IncreaseSpeedForSeconds(_increaseSpeed, IncreaseSpeedSeconds);
        }

    }
}