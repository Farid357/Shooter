using System;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Player;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PlayerRoot : MonoBehaviour, IPlayerRoot
    {
        private readonly SystemUpdate _systemUpdate = new();
        private IUpdateble _lastPlayer;
        private IInventory<IPotion> _potionsInventory;
        private IInventory<IGrenade> _grenadesInventory;

        public void Init(IInventory<IPotion> potionsInventory, IInventory<IGrenade> grenadesInventory)
        {
            _potionsInventory = potionsInventory ?? throw new ArgumentNullException(nameof(potionsInventory));
            _grenadesInventory = grenadesInventory ?? throw new ArgumentNullException(nameof(grenadesInventory));
        }
        
        public void Compose(IWeaponInput weaponInput, IShootingWeapon weapon)
        {
            TryRemove(_lastPlayer);
            var player = new PlayerWithWeapon(weaponInput, weapon);
            Add(player);
        }

        public void Compose(IPotionInput potionInput, IPotion potion)
        {
            TryRemove(_lastPlayer);
            var player = new PlayerPotion(potionInput, potion, _potionsInventory);
            Add(player);
        }

        public void Compose(IWeaponInput weaponInput, IGrenade grenade)
        {
            TryRemove(_lastPlayer);
            var player = new PlayerWithGrenade(grenade, weaponInput, _grenadesInventory);
            Add(player);
        }

        private void Add(IUpdateble updateble)
        {
            _systemUpdate.Add(updateble);
            _lastPlayer = updateble;
        }
        
        private void TryRemove(IUpdateble updateble)
        {
            if(updateble is not null)
                _systemUpdate.Remove(updateble);
        }
        
        private void Update() => _systemUpdate.Update(Time.deltaTime);
    }
}