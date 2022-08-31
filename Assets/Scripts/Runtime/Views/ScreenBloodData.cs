using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    [Serializable]
    public struct ScreenBloodData
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        
        [field: SerializeField, ProgressBar(0, 100)] public int NeedHealthForSprite { get; private set; }
        
    }
}