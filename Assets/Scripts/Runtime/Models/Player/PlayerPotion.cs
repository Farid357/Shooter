using System;
using System.Linq;
using Shooter.GameLogic;
using Shooter.Model.Inventory;

namespace Shooter.Model
{
    public sealed class PlayerPotion : IUpdateble
    {
        private readonly IPotionInput _potionInput;
        private readonly IPotion _potion;
        private readonly IInventory<IPotion> _inventory;

        public PlayerPotion(IPotionInput potionInput, IPotion potion, IInventory<IPotion> inventory)
        {
            _potionInput = potionInput ?? throw new ArgumentNullException(nameof(potionInput));
            _potion = potion ?? throw new ArgumentNullException(nameof(potion));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public void Update(float deltaTime)
        {
            if (_potionInput.HasInputed && _potion.CanShoot)
            {
                _potion.Shoot();
                _inventory.Drop(_inventory.Slots.First(slot => slot.Item.Model == _potion));
            }
        }
    }
}