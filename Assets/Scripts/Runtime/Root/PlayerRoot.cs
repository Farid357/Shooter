using Shooter.GameLogic;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PlayerRoot : CompositeRoot
    {
        [SerializeField] private StandartBulletsFactory _bulletsFactory;
        [SerializeField] private ShotgunBulletsFactory _shotgunBulletsFactory;

        [SerializeField] private IHealthView _healthView;
        [SerializeField, ProgressBar(10, 100)] private int _characterHealth;
        [SerializeField] private CharacterDeathView _characterDeathView;
        [SerializeField] private HealthTransformView _characterTransformView;
        [SerializeField] private IToggle _soundToggle;
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private WeaponData _shotgun;
        [SerializeField, Min(3)] private int _maxItemsCount = 10;
        [SerializeField] private IInventoryView _inventoryView;
        [SerializeField] private ItemData _shotgunData;
        
        private SystemUpdate _systemUpdate;
        private CharacterMovementInput _movementInput;

        public override void Compose()
        {
            var pickups = FindObjectsOfType<WeaponPickup>();
            var inventory = new Inventory<IWeapon>(_maxItemsCount, _inventoryView);
            IWeaponFactory factory = new WeaponFactory(_shotgunBulletsFactory, _bulletsFactory);
            pickups.ForEach(pickup => pickup.Init(inventory, factory));
            _systemUpdate = new SystemUpdate();
            _bulletsFactory.Init(_systemUpdate);
            _movementInput = new CharacterMovementInput(_movement);
            _characterDeathView.Init(_soundToggle);
            var health = new Health(_characterHealth, _healthView);
            _characterTransformView.Init(health);
            var weapon = factory.CreateShotgun(_shotgun);
            var player = new Player.Player(new WeaponKeyboardInput(), weapon.Weapon);
            _systemUpdate.Add(player, _movementInput);
            inventory.Add(new Item<IWeapon>(_shotgunData, weapon.Weapon), 1);
        }

        private void Update() => _systemUpdate.Update(Time.deltaTime);

        private void FixedUpdate() => _movementInput.FixedUpdate(Time.fixedDeltaTime);
    }
}