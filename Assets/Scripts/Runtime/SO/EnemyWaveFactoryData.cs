using System;
using Shooter.GameLogic;
using UnityEngine;

namespace Shooter.Model
{
    [Serializable]
    public sealed class EnemyWaveFactoryData 
    {
        [field: SerializeField] public StandartEnemyFactory Factory { get; private set; }
        
        [field: SerializeField, Range(1, 1000)] public int EnemiesCount { get; private set; }

    }
}