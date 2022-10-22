using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Shop
{
    public abstract class GoodData : ScriptableObject, IGoodData
    {
        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField, PreviewField, LabelWidth(100f)] public Sprite Sprite { get; private set; }
        
        [field: SerializeField, MinValue(1)] public int Price { get; private set; }

        [field: SerializeField] public WalletType WalletForPay { get; private set; }
    }
}