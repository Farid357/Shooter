using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PlayerRoot : SerializedMonoBehaviour
    {
        [SerializeField] private IHealthView _healthView;
        [SerializeField, ProgressBar(10, 100)] private int _characterHealth;
        [SerializeField] private CharacterDeathView _characterDeathView;
        [SerializeField] private HealthTransformView _characterTransformView;
        [SerializeField] private IToggle _soundToggle;
        [SerializeField] private CharacterMovement _movement;
        [SerializeField, Range(10, 90)] private int _regeneration = 10;
        
        private SystemUpdate _systemUpdate;
        private CharacterMovementInput _movementInput;

        public IPlayer Compose(IWeapon weapon)
        {
            _systemUpdate = new SystemUpdate();
            _movementInput = new CharacterMovementInput(_movement);
            _characterDeathView.Init(_soundToggle);
            var health = new Health(_characterHealth, _healthView);
            _characterTransformView.Init(health);
            var player = new Player.Player(new WeaponKeyboardInput(), weapon);
           // IUpdateble regeneration = new Regeneration(health, _regeneration);
            _systemUpdate.Add(player, _movementInput);
            return player;
        }

        private void Update() => _systemUpdate.Update(Time.deltaTime);

        private void FixedUpdate() => _movementInput.FixedUpdate(Time.fixedDeltaTime);
    }
}