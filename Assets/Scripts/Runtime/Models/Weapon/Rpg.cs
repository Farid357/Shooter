using System;

namespace Shooter.Model
{
    public sealed class Rpg : IWeapon
    {
        private readonly IWeapon _weapon;

        public Rpg(IWeapon weapon)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public int Bullets => _weapon.Bullets;
        
        public bool CanShoot => _weapon.CanShoot;
        
        public void Shoot() => _weapon.Shoot();

        public void VisualizeBullets() => _weapon.VisualizeBullets();

        public void AddBullets(int bullets) => _weapon.AddBullets(bullets);
    }
}