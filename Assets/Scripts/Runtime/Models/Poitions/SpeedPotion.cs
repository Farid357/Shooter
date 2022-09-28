using System;

namespace Shooter.Model
{
    public sealed class SpeedPotion : IPotion
    {
        private readonly ICharacterMovement _movement;
        private readonly IPotionView _view;
        private readonly float _increaseSpeed;
        private const float IncreaseSpeedSeconds = 2f;
        
        public SpeedPotion(ICharacterMovement movement, IPotionView view)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            var randomNumber = (float) new Random().NextDouble() * 2;
            _increaseSpeed = randomNumber < 1.2f ? 1.2f : randomNumber;
        }

        public bool CanShoot { get; private set; } = true;
        
        public bool HasShot { get; private set; }

        public async void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException(nameof(Shoot));

            HasShot = true;
            CanShoot = false;
            await _view.VisualizeShot();
            _movement.IncreaseSpeedForSeconds(_increaseSpeed, IncreaseSpeedSeconds);
        }

    }
}