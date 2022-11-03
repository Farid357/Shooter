using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class PotionFactory : IFactory<IPotion>
    {
        private readonly IPotion[] _potions;
        private IPotion _lastCreatedPotion;
        
        public PotionFactory(IPotion[] potions)
        {
            _potions = potions ?? throw new ArgumentNullException(nameof(potions));
        }

        public IPotion Create()
        {
            var randomIndex =  new Random().Next(0, _potions.Length);
            var randomPotion = _potions[randomIndex];
            
            if (_lastCreatedPotion is not null && randomPotion == _lastCreatedPotion)
            {
                randomPotion = randomIndex == _potions.Length ? _potions[randomIndex - 1] : _potions[randomIndex + 1];
            }

            return randomPotion;
        }
    }
}