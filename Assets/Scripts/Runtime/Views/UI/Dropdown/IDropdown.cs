using System;
using System.Collections.Generic;

namespace Shooter.GameLogic.Settings
{
    public interface IDropdown<out TType>
    {
        event Action<TType> OnSelected;
        
        IEnumerable<TType> Data { get; }
    }
}