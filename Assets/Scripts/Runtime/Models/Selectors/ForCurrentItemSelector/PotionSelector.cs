using System;
using Shooter.GameLogic;
using Shooter.Root;

namespace Shooter.Model.Inventory
{
    public sealed class PotionSelector : IInventoryItemSelector<IPotion>
    {
        private readonly IPlayerRoot _playerRoot;
        private readonly IPotionInput _potionInput;
        
        public PotionSelector(IPlayerRoot playerRoot, IPotionInput potionInput)
        {
            _playerRoot = playerRoot ?? throw new ArgumentNullException(nameof(playerRoot));
            _potionInput = potionInput ?? throw new ArgumentNullException(nameof(potionInput));
        }

        public void Select(IPotion potion)
        {
            _playerRoot.Compose(_potionInput, potion);
        }

        public void Unselect()
        {
            _playerRoot.Compose(new DummyPotionInput(), new DummyPotion());
        }
    }
}