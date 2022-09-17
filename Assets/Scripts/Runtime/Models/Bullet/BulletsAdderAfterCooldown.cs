using System;
using System.Collections.Generic;
using System.Linq;

namespace Shooter.Model
{
    public sealed class BulletsAdderAfterCooldown : IUpdateble
    {
        private readonly IEnumerable<IWeapon> _weapons;
        private readonly ITimer _timer;

        public BulletsAdderAfterCooldown(IEnumerable<IWeapon> weapons, ITimer timer)
        {
            _weapons = weapons ?? throw new ArgumentNullException(nameof(weapons));
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
        }

        public void Update(float deltaTime)
        {
            if (_timer.IsEnded)
            {
                AddBullets(_weapons);
            }
        }

        private void AddBullets(IEnumerable<IWeapon> weapons)
        {
            weapons = weapons.Where(weapon => weapon.Bullets <= 30);
            var count = weapons.Count() >= 3 ? 3 : weapons.Count();

            for (var i = 0; i < count; i++)
            {
                weapons.ElementAt(i).AddBullets(30);
            }
        }
    }
}