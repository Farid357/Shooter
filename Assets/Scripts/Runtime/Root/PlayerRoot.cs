using System;
using Shooter.GameLogic;
using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PlayerRoot : CompositeRoot
    {
        [SerializeField] private BulletsFactory _bulletsFactory;
        [SerializeField] private IBulletsView _bulletsView;
        [SerializeField] private IHealthView _healthView;
        [SerializeField, ProgressBar(10, 100)] private int _characterHealth;
        [SerializeField] private DeathView _deathView;
        [SerializeField] private HealthCollision _characterCollision;
        [SerializeField] private IToggle _soundToggle;
        [SerializeField] private IShotSound _shotSound;
        [SerializeField] private CharacterMovement _movement;

        private Player.Player _player;
        private CharacterMovementInput _movementInput;

        public override void Compose()
        {
            _movementInput = new CharacterMovementInput(_movement);
            _deathView.Init(_soundToggle);
            var health = new Health(_characterHealth, _healthView);
            _characterCollision.Init(health);
            var weapon = new WeaponWithRollback(new Weapon(_bulletsFactory, _shotSound), _bulletsView, 50);
            _player = new Player.Player(new WeaponKeyboardInput(), weapon);
        }

        private void Update()
        {
            _player.Update(Time.deltaTime);
            _movementInput.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _movementInput.FixedUpdate(Time.fixedDeltaTime);
        }
    }
}