using System;
using System.Collections.Generic;
using Sirenix.Utilities;

namespace Shooter.Model
{
    public sealed class CharacterIncreaseBulletsReward : IReward
    {
        private readonly IEnumerable<IWeapon> _weapons;
        private readonly int _addBullets;

        public CharacterIncreaseBulletsReward(IEnumerable<IWeapon> weapons)
        {
            _weapons = weapons ?? throw new ArgumentNullException(nameof(weapons));
        }

        public void Apply()
        {
            _weapons.ForEach(weapon => weapon.AddBullets(weapon.StartBullets / 2));
        }
    }
}