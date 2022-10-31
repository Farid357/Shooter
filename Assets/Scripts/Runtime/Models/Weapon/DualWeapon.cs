using System;
using System.Collections.Generic;
using System.Linq;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class DualWeapon : IWeapon
    {
        private readonly List<IWeapon> _weapons;

        public DualWeapon(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            if (firstWeapon is null)
                throw new ArgumentNullException(nameof(firstWeapon));
            
            if (secondWeapon is null) 
                throw new ArgumentNullException(nameof(secondWeapon));
            
            _weapons = new List<IWeapon>{ firstWeapon, secondWeapon };
        }
        
        public bool CanShoot => _weapons.Any(weapon => weapon.CanShoot);

        public int Bullets => _weapons.Sum(weapon => weapon.Bullets);

        public int StartBullets => _weapons.Sum(weapon => weapon.StartBullets);

        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException("Can't shoot!");
            
            _weapons.Where(weapon => weapon.CanShoot).ForEach(weapon =>
            {
                weapon.Shoot();
                weapon.VisualizeBullets();
            });
        }
        
        public void VisualizeBullets() => _weapons.ForEach(weapon => weapon.VisualizeBullets());

        public void AddBullets(int bullets)
        {
            bullets /= 2;
            _weapons.ForEach(weapon => weapon.AddBullets(bullets));
        }
    }
}