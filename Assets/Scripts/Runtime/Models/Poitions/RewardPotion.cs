using System;

namespace Shooter.Model
{
    public sealed class RewardPotion : IShootingWeapon
    {
        private readonly IPotionView _view;
        private readonly IReward _reward;

        public RewardPotion(IPotionView view, IReward reward)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _reward = reward ?? throw new ArgumentNullException(nameof(reward));
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
            _reward.Apply();
        }

    }
}