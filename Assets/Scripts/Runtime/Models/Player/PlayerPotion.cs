using System;
using Shooter.GameLogic;

namespace Shooter.Model
{
    public sealed class PlayerPotion : IUpdateble
    {
        private readonly IPotionInput _potionInput;
        private readonly IPotion _potion;

        public PlayerPotion(IPotionInput potionInput, IPotion potion)
        {
            _potionInput = potionInput ?? throw new ArgumentNullException(nameof(potionInput));
            _potion = potion ?? throw new ArgumentNullException(nameof(potion));
        }

        public void Update(float deltaTime)
        {
            if (_potionInput.HasInputed)
            {
                _potion.Shoot();
            }
        }
    }
}