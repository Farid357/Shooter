using Shooter.GameLogic;
using Shooter.Model;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PlayerRoot : CompositeRoot
    {
        [SerializeField] private BulletsFactory _bulletsFactory;
        private Player.Player _player;
        private WeaponWithRollback _weapon;

        public override void Compose()
        {
            _weapon = new WeaponWithRollback(new Weapon(_bulletsFactory), 10, 0.5f);
            _player = new Player.Player(new WeaponKeyboardInput(), _weapon);
        }

        private void Update()
        {
            _player.Update(Time.deltaTime);
            _weapon.Update(Time.deltaTime);
        }
    }
}