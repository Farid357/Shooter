using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Player;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PlayerRoot : MonoBehaviour, IPlayerRoot
    {
        private readonly SystemUpdate _systemUpdate = new();
        private PlayerWeapon _lastPlayer;
        private PlayerPotion _lastPlayerPotion;
        
        public void Compose(IWeaponInput weaponInput, IShootingWeapon weapon)
        {
            if (_lastPlayer is not null)
                _systemUpdate.Remove(_lastPlayer);
            
            var player = new PlayerWeapon(weaponInput, weapon);
            _systemUpdate.Add(player);
            _lastPlayer = player;
        }

        public void Compose(IPotionInput potionInput, IPotion potion)
        {
            if(_lastPlayerPotion is not null)
                _systemUpdate.Remove(_lastPlayerPotion);
            
            var playerPotion = new PlayerPotion(potionInput, potion);
            _systemUpdate.Add(playerPotion);
            _lastPlayerPotion = playerPotion;
        }
        
        private void Update() => _systemUpdate.Update(Time.deltaTime);
    }
}