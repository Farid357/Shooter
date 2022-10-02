using System;
using System.Threading.Tasks;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class WeaponWithShotWaiting : IWeapon
    {
        private readonly IWeapon _weapon;
        private readonly float _waitingSeconds;
        private bool _notNeedWaiting = true;

        public WeaponWithShotWaiting(IWeapon weapon, float waitingSeconds)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _waitingSeconds = waitingSeconds.TryThrowLessThanOrEqualsToZeroException();
        }

        public bool CanShoot => _weapon.CanShoot && _notNeedWaiting;

        public int StartBullets => _weapon.StartBullets;
        
        public void VisualizeBullets() => _weapon.VisualizeBullets();

        public void AddBullets(int bullets) => _weapon.AddBullets(bullets);

        public int Bullets => _weapon.Bullets;

        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException(nameof(Shoot));

            _weapon.Shoot();
            Wait();
        }

        private async void Wait()
        {
            _notNeedWaiting = false;
            await Task.Delay(TimeSpan.FromSeconds(_waitingSeconds));
            _notNeedWaiting = true;
        }
    }
}