using System;
using Shooter.GameLogic.Inventory;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class PotionFactory : IFactory<(IPotion, IInventoryItemGameObjectView)>
    {
        private readonly IPotion[] _potions;
        private readonly IFactory<IInventoryItemGameObjectView> _itemGameObjectViewFactory;
        private IPotion _lastCreatedPotion;
        
        public PotionFactory(IPotion[] potions, IFactory<IInventoryItemGameObjectView> itemGameObjectViewFactory)
        {
            _potions = potions ?? throw new ArgumentNullException(nameof(potions));
            _itemGameObjectViewFactory = itemGameObjectViewFactory ?? throw new ArgumentNullException(nameof(itemGameObjectViewFactory));
        }

        public (IPotion, IInventoryItemGameObjectView) Create()
        {
            var randomIndex =  new Random().Next(0, _potions.Length);
            var randomPotion = _potions[randomIndex];
            
            if (_lastCreatedPotion is not null && randomPotion == _lastCreatedPotion)
            {
                randomPotion = randomIndex == _potions.Length ? _potions[randomIndex - 1] : _potions[randomIndex + 1];
            }

            return (randomPotion, _itemGameObjectViewFactory.Create());
        }
    }
}