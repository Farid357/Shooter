using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Shop
{
    public abstract class GoodData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField, HideLabel, PreviewField, LabelWidth(70f)] public Sprite Sprite { get; private set; }
        
        [field: SerializeField, MinValue(1)] public int Price { get; private set; }

    }
}