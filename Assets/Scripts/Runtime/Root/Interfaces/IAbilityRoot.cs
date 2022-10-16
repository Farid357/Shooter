using System.Collections.Generic;
using Shooter.Model;

namespace Shooter.Root
{
    public interface IAbilityRoot
    {
        IEnumerable<IAbility> Abilities();
    }
}