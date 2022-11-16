using System;
using Shooter.Model.Inventory;

namespace Shooter.Model
{
    public sealed class RewardPotion : IPotion
    {
        private readonly IPotionView _view;
        private readonly IReward _reward;

        public RewardPotion(IPotionView view, IReward reward, IInventoryItemGameObjectView itemView)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _reward = reward ?? throw new ArgumentNullException(nameof(reward));
            ItemView = itemView ?? throw new ArgumentNullException(nameof(itemView));
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
            _reward.Apply();
        }

    }
}