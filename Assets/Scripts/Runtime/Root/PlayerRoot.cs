using System;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PlayerRoot : SerializedMonoBehaviour, IPlayerRoot
    {
        [SerializeField] private PotionRoot _potionRoot;
        [SerializeField] private GrenadeSelectorRoot _grenadeSelectorRoot;
        private readonly SystemUpdate _systemUpdate = new();
        private IUpdateble _lastPlayer;
        private IInventory<IGrenade> _grenadesInventory;
        private IInventory<(IWeapon, IWeaponInput)> _weaponsInventory;
        private IInventoryItemSelector<(IWeapon, IWeaponInput)> _weaponSelector;

        public IDroppingWeapon ComposedDroppingWeapon { get; private set; }
        
        public void Init(IInventory<(IWeapon, IWeaponInput)> weaponsInventory, IInventory<IGrenade> grenadesInventory, IInventoryItemSelector<(IWeapon, IWeaponInput)> weaponSelector)
        {
            _weaponSelector = weaponSelector ?? throw new ArgumentNullException(nameof(weaponSelector));
            _weaponsInventory = weaponsInventory ?? throw new ArgumentNullException(nameof(weaponsInventory));
            _grenadesInventory = grenadesInventory ?? throw new ArgumentNullException(nameof(grenadesInventory));
        }
        
        public void Compose(IWeaponInput weaponInput, IWeapon weapon)
        {
            TryRemove(_lastPlayer);
            var player = new PlayerWithWeapon(weaponInput, weapon);
            Add(player);
        }

        public void Compose(IWeaponInput potionInput, IPotion potion) => Compose(potionInput, potion, _potionRoot.Compose(), _potionRoot.Selector);

        public void Compose(IWeaponInput weaponInput, IGrenade grenade) => Compose(weaponInput, grenade, _grenadesInventory, _grenadeSelectorRoot.Compose());

        private void Compose<TWeapon>(IWeaponInput weaponInput, TWeapon weapon, IInventory<TWeapon> inventory, IInventoryItemSelector<TWeapon> droppingWeaponSelector) where TWeapon : IDroppingWeapon
        {
            TryRemove(_lastPlayer);
            var player = new PlayerWithDroppingWeapon<TWeapon>(weapon, weaponInput, inventory, _weaponsInventory, droppingWeaponSelector, _weaponSelector);
            ComposedDroppingWeapon = weapon;
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