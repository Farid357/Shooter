using System;
using UnityEngine;

namespace Shooter.GameLogic
{
    [Serializable]
    public struct EnemyAnimationData
    {
        [field: SerializeField, Range(0, 100)] public int NeedHealth { get; private set; }

        [field: SerializeField, TextArea] public string Name { get; private set; }
    }
}