using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class DummyFactoryFromShootingWeapon : IFactory<IWeapon>
    {
        private readonly IFactory<IShootingWeapon> _factory;

        public DummyFactoryFromShootingWeapon(IFactory<IShootingWeapon> grenadeFactory)
        {
            _factory = grenadeFactory ?? throw new ArgumentNullException(nameof(grenadeFactory));
        }

        public IWeapon Create()
        {
            return new DummyWeaponFromShootingWeapon(_factory.Create());
        }

        private class DummyWeaponFromShootingWeapon : IWeapon
        {
            private readonly IShootingWeapon _weapon;

            public DummyWeaponFromShootingWeapon(IShootingWeapon weapon)
            {
                _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            }

            public void Shoot()
            {
                if (CanShoot == false)
                    throw new InvalidOperationException(nameof(Shoot));
                
                _weapon.Shoot();
            }

            public bool CanShoot => _weapon.CanShoot;

            public int Bullets => 50000000;

            public int StartBullets => 50000000;
            
            public void VisualizeBullets()
            {
            }

            public void AddBullets(int bullets)
            {
            }
        }
    }
}