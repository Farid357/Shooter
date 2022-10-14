using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class CharacterRoot : CompositeRoot
    {
        [SerializeField] private IHealthView _healthView;
        [SerializeField, ProgressBar(10, 100)] private int _characterHealth;
        [SerializeField] private HealthTransformView _characterTransformView;
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private CharacterArmorView _characterArmorView;
        
        private SystemUpdate _systemUpdate;
        private CharacterMovementInput _movementInput;
        
        public override void Compose()
        {
            _systemUpdate = new SystemUpdate();
            _movementInput = new CharacterMovementInput(_movement);
            var armorStorage = new StorageWithNameSaveObject<CharacterHealthView, int>(new BinaryStorage());
            IHealth health;

            if (armorStorage.HasSave())
            {
                health = new Health(_characterHealth, _healthView);
            }
            else
            {
                health = new Armor(new Health(_characterHealth, _healthView), _characterArmorView, armorStorage.Load());
            }
            
            _characterTransformView.Init(health);
            _systemUpdate.Add(_movementInput);
        }
        
        private void Update() => _systemUpdate.Update(Time.deltaTime);

        private void FixedUpdate() => _movementInput.FixedUpdate(Time.fixedDeltaTime);
    }
}