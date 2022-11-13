using System;
using System.Collections.Generic;
using System.Linq;

namespace Shooter.Model
{
    public sealed class RandomWeaponFactory : IRandomWeaponFactory
    {
        private readonly Random _random = new();
        private readonly IEnumerable<(IWeapon, IWeaponInput)> _weapons;
        private (IWeapon, IWeaponInput) _lastWeapon;
        
        public RandomWeaponFactory(IEnumerable<(IWeapon, IWeaponInput)> weapons)
        {
            _weapons = weapons ?? throw new ArgumentNullException(nameof(weapons));
        }

        public (IWeapon, IWeaponInput) Get()
        {
            var randomWeapon = GetRandom();
            
            if (_lastWeapon.Item1 != null && _lastWeapon == randomWeapon)
            {
                randomWeapon = GetRandom();
            }

            _lastWeapon = randomWeapon;
            return randomWeapon;
        }

        private (IWeapon, IWeaponInput) GetRandom()
        {
            var randomIndex = _random.Next(0, _weapons.Count());
            var randomWeapon = _weapons.ElementAt(randomIndex);
            return randomWeapon;
        }
    }
}