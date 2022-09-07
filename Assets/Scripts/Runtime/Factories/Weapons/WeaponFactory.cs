using System;
using Shooter.Model;
using Shooter.Tools;

namespace Shooter.GameLogic
{
    public sealed class WeaponFactory : IWeaponFactory
    {
        private readonly IFactory<IBullet> _standartFactory;
        private readonly IFactory<IBullet> _shotgunFactory;

        public WeaponFactory(IFactory<IBullet> shotgunFactory, IFactory<IBullet> standartFactory)
        {
            _shotgunFactory = shotgunFactory ?? throw new ArgumentNullException(nameof(shotgunFactory));
            _standartFactory = standartFactory ?? throw new ArgumentNullException(nameof(standartFactory));
        }

        public (IWeapon, IWeaponWithRollback) CreateShotgun(WeaponData data) => Create(data, _shotgunFactory);

        public (IWeapon, IWeaponWithRollback) CreateAk74(WeaponData data) => Create(data, _standartFactory);

        private (IWeapon, IWeaponWithRollback) Create(WeaponData data, IFactory<IBullet> factory)
        {
            var weapon = new Weapon(factory, data.ShotView);
            var shotgunRollback = new WeaponWithRollback(weapon, data.BulletsView, data.Bullets);
            var shotgun = new WeaponWithShotWaiting(shotgunRollback, data.WaitSeconds);
            return (shotgun, shotgunRollback);
        }
    }
}