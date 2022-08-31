using Shooter.GameLogic;
using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PlayerRoot : CompositeRoot
    {
        [SerializeField] private BulletsFactory _bulletsFactory;
        [SerializeField] private IView<int> _bulletsView;
        [SerializeField] private IView<int> _healthView;
        [SerializeField, ProgressBar(10, 100)] private int _characterHealth;
        [SerializeField] private DeathView _deathView;
        [SerializeField] private HealthCollision _characterCollision;
        [SerializeField] private IToggle _soundToggle;
        [SerializeField] private IShotSound _shotSound;
        [SerializeField] private CharacterMovement _movement;
        
        private Player.Player _player;
        private CharacterInput _input;

        public override void Compose()
        {
            _input = new CharacterInput(_movement);
            _deathView.Init(_soundToggle);
            var health = new Health(_characterHealth, _healthView, _deathView);
            _characterCollision.Init(health);
            var weapon = new WeaponWithRollback(new Weapon(_bulletsFactory, _shotSound), 50,  _bulletsView);
            _player = new Player.Player(new WeaponKeyboardInput(), weapon);
        }

        private void Update()
        {
            _player.Update(Time.deltaTime);
            _input.Update(Time.deltaTime);
        }
    }
}