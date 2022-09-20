using Shooter.GameLogic;
using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class CharacterRoot : CompositeRoot
    {
        [SerializeField] private IHealthView _healthView;
        [SerializeField, ProgressBar(10, 100)] private int _characterHealth;
        [SerializeField] private CharacterDeathView _characterDeathView;
        [SerializeField] private HealthTransformView _characterTransformView;
        [SerializeField] private IToggle _soundToggle;
        [SerializeField] private CharacterMovement _movement;

        private SystemUpdate _systemUpdate;
        private CharacterMovementInput _movementInput;

        
        public override void Compose()
        {
            _systemUpdate = new SystemUpdate();
            _movementInput = new CharacterMovementInput(_movement);
            _characterDeathView.Init(_soundToggle);
            var health = new Health(_characterHealth, _healthView);
            _characterTransformView.Init(health);
            _systemUpdate.Add(_movementInput);
        }
        
        private void Update() => _systemUpdate.Update(Time.deltaTime);

        private void FixedUpdate() => _movementInput.FixedUpdate(Time.fixedDeltaTime);
    }
}