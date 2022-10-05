﻿using Shooter.Model;

namespace Shooter.Root
{
    public interface IScoreRoot
    {
        IScore Score { get; }
        
        IScore ComposeScore();
        
    }
}