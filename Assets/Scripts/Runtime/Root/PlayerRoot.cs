using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PlayerRoot : SerializedMonoBehaviour, IPlayerRoot
    {
        private readonly SystemUpdate _systemUpdate = new();
        private Player.Player _lastPlayer;

        public void Compose(IWeapon weapon, IWeaponInput weaponInput)
        {
            if (_lastPlayer is not null)
                _systemUpdate.Remove(_lastPlayer);

            var player = new Player.Player(weaponInput, weapon);
            _systemUpdate.Add(player);
            _lastPlayer = player;
        }

        private void Update() => _systemUpdate.Update(Time.deltaTime);
    }
}