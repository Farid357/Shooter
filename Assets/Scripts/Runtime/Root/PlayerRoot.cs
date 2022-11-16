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
        private readonly SystemUpdate _systemUpdate = new();
        private IUpdateble _lastPlayer;
        private IInventory<IThrowingWeapon> _grenadesInventory;
        private IInventory<(IWeapon, IWeaponInput)> _weaponsInventory;
        private IInventoryItemsSelector _weaponSelector;
        private IInventoryItemsSelector _grenadeSelector;

        public IThrowingWeapon ComposedThrowingWeapon { get; private set; } = new DummyPotion();
        
        public void Init(IInventory<(IWeapon, IWeaponInput)> weaponsInventory, IInventory<IThrowingWeapon> grenadesInventory, IInventoryItemsSelector weaponSelector, 
            IInventoryItemsSelector grenadeSelector)
        {
            _grenadeSelector = grenadeSelector;
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

        public void Compose(IWeaponInput potionInput, IPotion potion) => Compose(potionInput, potion, _potionRoot.Compose(), _grenadeSelector);

        public void Compose(IWeaponInput weaponInput, IThrowingWeapon throwingWeapon) => Compose(weaponInput, throwingWeapon, _grenadesInventory, _grenadeSelector);

        private void Compose<TWeapon>(IWeaponInput weaponInput, TWeapon weapon, IInventory<TWeapon> inventory, IInventoryItemsSelector droppingWeaponSelector) where TWeapon : IThrowingWeapon
        {
            TryRemove(_lastPlayer);
            var player = new PlayerWithDroppingWeapon<TWeapon>(weapon, weaponInput, inventory, _weaponsInventory, droppingWeaponSelector, _weaponSelector);
            ComposedThrowingWeapon = weapon;
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