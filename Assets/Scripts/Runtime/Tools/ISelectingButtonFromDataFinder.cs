﻿using Shooter.GameLogic;

namespace Shooter.Shop
{
    public interface ISelectingButtonFromDataFinder
    {
        IButton Find(IGoodData data);
    }
}