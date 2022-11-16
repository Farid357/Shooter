using System.Collections.Generic;
using System.Linq;
using Shooter.GameLogic;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class InventoriesRoot : CompositeRoot
    {
        [SerializeField] private IFactory<IBullet> _shotgunBulletsFactory;
        [SerializeField] private IInventoryView _inventoryView;
        [SerializeField] private Dictionary<KeyCode, int> _keypadNumbers;
        [SerializeField] private PlayerRoot _playerRoot;
        [SerializeField] private PickupsRoot _pickupsRoot;
        [SerializeField] private IInventoryView _grenadesInventoryView;
        [SerializeField] private ItemData _grenadeItem;
        [SerializeField] private Dictionary<KeyCode,int> _grenadeInventoryKeypadNumbers;
        [SerializeField] private IFactory<IInventoryItemGameObjectView> _grenadeGameObjectViewFactory;
        [SerializeField] private IFactory<IThrowingWeapon> _grenadeFactory;
        [SerializeField] private IBulletsView _secondWeaponBulletsView;
        [SerializeField] private PotionRoot _potionRoot;
        [SerializeField] private GrenadeSelectorRoot _grenadeSelectorRoot;

        [VerticalGroup("Start Weapon")] [SerializeField] private ItemData _weaponItemData;
        [VerticalGroup("Start Weapon")] [SerializeField] private WeaponData _startWeaponData;
        [VerticalGroup("Start Weapon")] [SerializeField] private InventoryItemGameObjectView _weaponView;

        private readonly SystemUpdate _systemUpdate = new();

        public override void Compose()
        {
            IWeaponFactory factory = new WeaponFactoryWithShootWaiting(_shotgunBulletsFactory, _startWeaponData);
            var weapon = factory.Create();
            var weaponsInventory = new Inventory<(IWeapon, IWeaponInput)>(_inventoryView);
            var weaponSelector = new WeaponSelector(_playerRoot, _secondWeaponBulletsView);
            var item = new Item<(IWeapon, IWeaponInput)>(_weaponItemData, (weapon, new BurstWeaponInput()), _weaponView);
            var slot = new InventorySlot<(IWeapon, IWeaponInput)>(weaponSelector, item);
            var grenadesInventory = new Inventory<IThrowingWeapon>(_grenadesInventoryView, 3);
            var grenade = _grenadeFactory.Create();
            var grenadeSlot = new InventorySlot<IThrowingWeapon>(_grenadeSelectorRoot.Compose(), new List<Item<IThrowingWeapon>>
            {
                CreateGrenadeItem(),
                CreateGrenadeItem(),
            }, 2);
            
            grenadesInventory.Add(grenadeSlot);
            weaponsInventory.Add(slot);
            var weaponInventoryItemsSelector = new InventoryItemsSelector<(IWeapon, IWeaponInput)>(weaponsInventory);
            var grenadeInventorySelector = new InventoryItemsSelector<IThrowingWeapon>(grenadesInventory);
            var potionsInventory = _potionRoot.Compose();
            _playerRoot.Init(weaponsInventory, grenadesInventory, weaponInventoryItemsSelector, grenadeInventorySelector);
            var potionInventorySelector = new InventoryItemsSelector<IPotion>(potionsInventory);
            var selectors = new List<IInventoryItemsSelector>{ grenadeInventorySelector, weaponInventoryItemsSelector, potionInventorySelector };
            var inventoryItemsInputSelector = new InventoryItemsSelectorInput(_keypadNumbers, selectors, weaponInventoryItemsSelector);
            var grenadeInputSelector = new InventoryItemsSelectorInput(_grenadeInventoryKeypadNumbers, selectors, grenadeInventorySelector);
            var potionInputSelector = new InventoryItemsSelectorInput(_potionRoot.KeypadNumbers, selectors, potionInventorySelector);

            _weaponView.Show();
            _systemUpdate.Add(inventoryItemsInputSelector, grenadeInputSelector, potionInputSelector);
            _pickupsRoot.Compose(weaponsInventory, grenadesInventory);
            weaponInventoryItemsSelector.Select(0);
            var bulletsAddTimer = new IndependentTimer(new DummySecondsView(), 10);
            
            _systemUpdate.Add(new BulletsAdderAfterCooldown(weaponsInventory.Slots.Select(model => model.Item.Model.Item1), 
                bulletsAddTimer), new InventoryItemsDropInput<(IWeapon, IWeaponInput)>(_keypadNumbers, weaponsInventory));
        }

        private Item<IThrowingWeapon> CreateGrenadeItem()
        {
            var grenade = _grenadeFactory.Create();
            var grenadeItem = new Item<IThrowingWeapon>(_grenadeItem, grenade, grenade.ItemView);
            return grenadeItem;
        }

        private void Update() => _systemUpdate.Update(Time.deltaTime);
    }
}