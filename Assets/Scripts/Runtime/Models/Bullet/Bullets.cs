using System;
using System.Collections.Generic;
using Sirenix.Utilities;

namespace Shooter.Model
{
    public sealed class Bullets : IBullet
    {
        private readonly IEnumerable<IBullet> _all;

        public Bullets(IEnumerable<IBullet> all)
        {
            _all = all ?? throw new ArgumentNullException(nameof(all));
        }

        public void Throw() => _all.ForEach(bullet => bullet.Throw());
        
    }
}
